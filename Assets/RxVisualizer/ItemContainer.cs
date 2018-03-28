using System.Collections.Generic;

namespace RxVisualizer{
    public class ItemContainer : IContainer<Item>{
        private readonly List<Item> items;

        public ItemContainer(){
            items = new List<Item>();
        }

        public void Add(Item item){
            items.Add(item);
        }

        public List<Item> GetItems(){
            return items;
        }
    }
}