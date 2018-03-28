using System.Collections.Generic;

namespace RxVisualizer{
    public interface IContainer<T>{
        void Add(T item);
        List<T> GetItems();
        void Clear();
    }
}