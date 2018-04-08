using System;
using UniRx;

namespace RxVisualizer{
    public static class VisualizerExtensionMethods{
        
        /// Подписываемся на события для непосредственной обработки.
        /// Подписка на события инициирует работу субъекта подписки, что может вызывать сайд эффект.
        /// <param name="name">Имя для визуализируемой последовательности,
        /// чтобы отличать одну от другой и возможности именования пользователем</param>
        public static IDisposable Visualize<T>(this IObservable<T> obs, string name){
            return obs.Visualize(name, mapper:MarkMapper<T>.Default);
        }
        
        /// Подписываемся на события для непосредственной обработки.
        /// Подписка на события инициирует работу субъекта подписки, что может вызывать сайд эффект.
        /// <param name="name">Имя для визуализируемой последовательности,
        /// чтобы отличать одну от другой и возможности именования пользователем</param>
        /// <param name="mapper">Инструмент позволяющий для определенных событий задать определенный цвет при отрисовке</param>
        public static IDisposable Visualize<T>(this IObservable<T> obs,string name, MarkMapper<T> mapper){
            return obs.Visualize(name, data => data.ToString(), mapper);
        }
        
        /// Подписываемся на события для непосредственной обработки.
        /// Подписка на события инициирует работу субъекта подписки, что может вызывать сайд эффект.
        /// <param name="name">Имя для визуализируемой последовательности,
        /// чтобы отличать одну от другой и возможности именования пользователем</param>
        /// <param name="ToString">Функция для преобразования данных о событии в строку</param>
        public static IDisposable Visualize<T>(this IObservable<T> obs,string name, Func<T,string> ToString){
            return obs.Visualize(name, ToString, MarkMapper<T>.Default);
        }
        
        /// Подписываемся на события для непосредственной обработки.
        /// Подписка на события инициирует работу субъекта подписки, что может вызывать сайд эффект.
        /// <param name="name">Имя для визуализируемой последовательности,
        /// чтобы отличать одну от другой и возможности именования пользователем</param>
        /// <param name="ToString">Функция для преобразования данных о событии в строку</param>
        /// <param name="mapper">Инструмент позволяющий для определенных событий задать определенный цвет при отрисовке</param>
        public static IDisposable Visualize<T>(
            this IObservable<T> obs, 
            string name, 
            Func<T,string> ToString, 
            MarkMapper<T> mapper){
            
            return obs.Subscribe(
                data => VisualizerItemHandler.Handle(name,ToString(data),mapper.GetMark(data)),
                exception => VisualizerItemHandler.Handle(name,exception),
                () => VisualizerItemHandler.Handle(name));
        }
        
    }
}