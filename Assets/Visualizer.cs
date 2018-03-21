using System;
using UniRx;

namespace DefaultNamespace{
    public static class Visualizer{

        public static IObservable<T> Visualize<T>(this IObservable<T> obs,string name){

            obs.Select(_ => Unit.Default).Subscribe(
                value => RxVisualizerWindow.OnNext(value, name), 
                ex => RxVisualizerWindow.OnError(ex,name), 
                () => RxVisualizerWindow.OnCompleted(name));
            
            return obs;
        }
    }
}