using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace RxVisualizer{
    public static class VisualizerExtensionMethods{
        
        public static IObservable<T> Visualize<T>(this IObservable<T> observable){
            InjectionData<T> data = new InjectionData<T>(){
                observable = observable,
                sequenceName = observable.GetHashCode().ToString(),
                stringifier = input => input.ToString(),
                mapper = MarkMapper<T>.Default
            };
            return Mediator(data);
        }
        
        public static IObservable<T> Visualize<T>(this IObservable<T> observable, string sequenceName){
            InjectionData<T> data = new InjectionData<T>(){
                observable = observable,
                sequenceName = sequenceName,
                stringifier = input => input.ToString(),
                mapper = MarkMapper<T>.Default
            };
            return Mediator(data);
        }
        
        public static IObservable<T> Visualize<T>(this IObservable<T> observable, string sequenceName, Func<T, string> stringifier){
            InjectionData<T> data = new InjectionData<T>(){
                observable = observable,
                sequenceName = sequenceName,
                stringifier = stringifier,
                mapper = MarkMapper<T>.Default
            };
            return Mediator(data);
        }
        
        public static IObservable<T> Visualize<T>(this IObservable<T> observable, string sequenceName, Func<T, string> stringifier, MarkMapper<T> mapper){
            InjectionData<T> data = new InjectionData<T>(){
                observable = observable,
                sequenceName = sequenceName,
                stringifier = stringifier,
                mapper = mapper
            };
            return Mediator(data);
        }
        
        /// <summary>
        /// Observer which is receiving every item from sequence and pushing it to VisualizerItemHandler
        /// </summary>
        /// <param name="sequenceName">Text for label</param>
        /// <param name="stringifier">Function which transform any input data to string</param>
        /// <param name="mapper">Mark appearance controller</param>
        /// <returns></returns>
        private static IObserver<T> GetReceiver<T>(InjectionData<T> injectionData){
            return Observer.Create<T>(
                    data => VisualizerItemHandler.Handle(injectionData.sequenceName,injectionData.stringifier(data),injectionData.mapper.GetMark(data)),
                    exception => VisualizerItemHandler.Handle(injectionData.sequenceName,exception),
                    () => VisualizerItemHandler.Handle(injectionData.sequenceName));
        }

        public struct InjectionData<T>{
            public IObservable<T> observable;
            public string sequenceName;
            public Func<T, string> stringifier;
            public MarkMapper<T> mapper;
        }

        public static IObservable<T> Mediator<T>(InjectionData<T> injectionData){
            //Reciever injection
            return injectionData.observable
                .DoOnSubscribe(() => Inject(injectionData))
                .DoOnCancel(() => Reject(injectionData.sequenceName));
        }

        /// Inject reciever for visualizator
        private static void Inject<T>(InjectionData<T> injectionData){
            if (MediatorSubscriptionHolder.HasSubscription(injectionData.sequenceName)) return; // Visualizer already injected
            
            var dis = injectionData.observable.Subscribe(GetReceiver(injectionData));
            MediatorSubscriptionHolder.Add(injectionData.sequenceName,dis);
            Debug.Log("Visualizer injected to sequence : "+injectionData.sequenceName);
        }

        private static void Reject(string sequenceName){
            MediatorSubscriptionHolder.Dispose(sequenceName);
            Debug.Log("Visualizer rejected from sequence : "+sequenceName);
        }

        
        
    }
}