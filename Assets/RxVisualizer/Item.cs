using System;

namespace RxVisualizer{
    
    // Точка на линии, обозначающая одно из событий Observable
    public struct Item{
        
        // Время от запуска игры, в момент которого было вызвано событие
        public float time;

        public string data;

        public Type type;

        public enum Type{
            next, error, completed
        }

    }
}