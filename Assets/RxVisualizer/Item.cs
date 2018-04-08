using System;

namespace RxVisualizer{
    
    // Точка на линии, обозначающая одно из событий Observable
    public struct Item{
        
        public enum Mark{
            Green = 0, Red = 1, Blue = 2, Orange = 3
        }
        
        public enum Type{
            Next, Error, Completed
        }

        // Время от запуска игры, в момент которого было вызвано событие
        public float time;

        public string data;

        public Type type;

        public Mark mark;

    }
}