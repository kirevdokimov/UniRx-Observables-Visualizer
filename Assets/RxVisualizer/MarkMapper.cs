using System;
using System.Collections.Generic;

namespace RxVisualizer{
    public class MarkMapper<T>{
        
        public static readonly MarkMapper<T> Default = new MarkMapper<T>(); // Нет правил, GetMark всегда вернет defaultMark
        
        private static Drawer.Mark defaultMark = Drawer.Mark.orage;

        private Dictionary<Func<T, bool>, Drawer.Mark> map;

        public MarkMapper(){
            map = new Dictionary<Func<T, bool>, Drawer.Mark>();
        }

        public Drawer.Mark GetMark(T data){
            foreach (var item in map){
                if (item.Key.Invoke(data)){
                    return item.Value;
                }
            }

            return defaultMark;
        }

        public void AddRule(Func<T, bool> rule, Drawer.Mark mark){
            map.Add(rule,mark);
        }
    }
}