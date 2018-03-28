using System;
using UniRx;
using UnityEngine;

namespace RxVisualizer{
    public static class Visualizer{

        public enum Stub{
            stub;
        }


        static void Handle<T>(string name, T data){
            
        }
        static void Handle(string name){
            
        }

        // Получает и возвращает данные как посредник
        static T Mediator<T>(string name, T data){  
            Handle(name,data);
            return data;
        }
        

        // Extensions
        public static IObservable<T> Visualize<T>(this IObservable<T> obs,string name){

            // Observable не начинает работу пока кто-нибудь не подпишется, поэтому, чтобы не подписываться
            // и не инициировать тем самым работу, мы ставим послерника в Select, что никак ре влияет на Observable
            return obs.Select(data => Mediator(name,data));
        }
        
        public static IDisposable VisualizeSub<T>(this IObservable<T> obs,string name){

            return obs.Subscribe(
                data => Handle(name,data),
                exception => Handle(name,exception),
                () => Handle(name));
            
        }
    }
}