using System;
using System.Collections.Generic;

namespace RxVisualizer{
    public static class MediatorSubscriptionHolder{
        private static Dictionary<string, IDisposable> subs;

        public static void Add(string name, IDisposable disposable){
            if(subs == null) subs = new Dictionary<string, IDisposable>();
            subs.Add(name,disposable);
        }

        public static void Dispose(string name){
            if (subs == null) return;
            if(HasSubscription(name)) subs[name].Dispose();
        }

        public static bool HasSubscription(string name){
            return subs != null && subs.ContainsKey(name);
        }
    }
}