﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace RxVisualizer{
    public static class VisualizerItemHandler{
        
        private static readonly Dictionary<string, IContainer<Item>> container = new Dictionary<string, IContainer<Item>>();

        public static IEnumerable<IContainer<Item>> Containers{
            get{ return container.Values; }
        }

        public static void Handle(string name, string data){
            var item = new Item(){
                data = data,
                type = Item.Type.next,
                time = Time.time
            };
            
            GetContainer(name).Add(item);
        }
        
        public static void Handle(string name, Exception ex){
            var item = new Item(){
                data = ex.Message,
                type = Item.Type.error,
                time = Time.time
            };
            
            GetContainer(name).Add(item);
        }
        
        public static void Handle(string name){
            var item = new Item(){
                data = "OnCompleted",
                type = Item.Type.error,
                time = Time.time
            };
            
            GetContainer(name).Add(item);
        }

        private static IContainer<Item> GetContainer(string name){
            if (container.ContainsKey(name))
                return container[name];

            IContainer<Item> cnt = new ItemContainer();
            container.Add(name,cnt);
            return cnt;
        }
        
    }
}