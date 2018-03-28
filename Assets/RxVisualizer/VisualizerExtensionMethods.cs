using System;
using UniRx;

namespace RxVisualizer{
    public static class VisualizerExtensionMethods{
        
        /// Подписываемся на события для непосредственной обработки.
        /// Подписка на события инициирует работу субъекта подписки, что может вызывать сайд эффект.
        /// <param name="name">Имя для визуализируемой последовательности,
        /// чтобы отличать одну от другой и возможности именования пользователем</param>
        public static IDisposable Visualize<T>(this IObservable<T> obs,string name){
            return obs.Visualize(name, data => data.ToString());
        }
        /// Подписываемся на события для непосредственной обработки.
        /// Подписка на события инициирует работу субъекта подписки, что может вызывать сайд эффект.
        /// <param name="name">Имя для визуализируемой последовательности,
        /// чтобы отличать одну от другой и возможности именования пользователем</param>
        public static IDisposable Visualize<T>(this IObservable<T> obs,string name,Func<T,string> ToString){
            return obs.Subscribe(
                data => VisualizerItemHandler.Handle(name,ToString(data)),
                exception => VisualizerItemHandler.Handle(name,exception),
                () => VisualizerItemHandler.Handle(name));
        }
        
// Не сможет понять тип события (next, exception, completed)
//        
//        // Получает и возвращает данные как посредник
//        static T Mediator<T>(string name, T data){  
//            Handle(name,data);
//            return data;
//        }
//        
//
//        /// Создаем посредника, который будет обрабатывать каждое событие.
//        /// <param name="name">Имя для визуализируемой последовательности,
//        /// чтобы отличать одну от другой и возможности именования пользователем</param>
//        public static IObservable<T> VisualizeMed<T>(this IObservable<T> obs,string name){
//            return obs.Select(data => {
//                Handle(name, data.ToString());
//                return data;});
//        }
//        /// Создаем посредника, который будет обрабатывать каждое событие.
//        /// <param name="name">Имя для визуализируемой последовательности,
//        /// чтобы отличать одну от другой и возможности именования пользователем</param>
//        /// <param name="ToString">Кастомизация перевода данных события в строку</param>
//        public static IObservable<T> VisualizeMed<T>(this IObservable<T> obs,string name,Func<T,string> ToString){
//            return obs.Select(data => {
//                Handle(name, ToString(data)); 
//                return data;});
//        }
        
    }
}