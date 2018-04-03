using System.Collections.Generic;

namespace RxVisualizer{
    public class ItemContainer : IContainer<Item>{

        private string name;
        
        private readonly List<Item> items;

        public ItemContainer(string name){
            items = new List<Item>();
            SetName(name);
        }

        public void Add(Item item){
            items.Add(item);
        }

        public List<Item> GetItems(){
            return items;
        }

        public void Clear(){
            items.Clear();
        }

        public void SetName(string value){
            name = value;
        }

        public string GetName(){
            return name;
        }
    }
}