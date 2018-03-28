using System;

namespace RxVisualizer{
    
    // Точка на линии, обозначающая одно из событий Observable
    public struct Point{
        
        // Время от запуска игры, в момент которого было вызвано событие
        public float time;

        public PointType type;

        public enum PointType{
            next, error, completed
        }

    }
}