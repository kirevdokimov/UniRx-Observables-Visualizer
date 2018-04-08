using System;
using System.Collections.Generic;
using Mark = RxVisualizer.Item.Mark;

namespace RxVisualizer{
    public class MarkMapper<T>{
        
        public static readonly MarkMapper<T> Default = new MarkMapper<T>(); // Нет правил, GetMark всегда вернет defaultMark

        private const Mark DefaultMark = Mark.Orange;

        private readonly Dictionary<Func<T, bool>, Mark> map;

        public MarkMapper(){
            map = new Dictionary<Func<T, bool>, Mark>();
        }

        public Mark GetMark(T data){
            foreach (var item in map){
                if (item.Key.Invoke(data)){
                    return item.Value;
                }
            }

            return DefaultMark;
        }

        public MarkMapper<T> AddRule(Func<T, bool> rule, Mark mark){
            map.Add(rule,mark);
            return this;
        }

        public MarkMapper<T> Any(Mark mark){
            return AddRule(i => true, mark);
        }
    }
}