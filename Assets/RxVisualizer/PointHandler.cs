using System.Collections.Generic;

namespace RxVisualizer{
    public class PointHandler : IPointHandler{
        
        public void Handle(string name, Point.PointType type){
            throw new System.NotImplementedException();
        }

        public void Handle(string name, Point.PointType type, object data){
            throw new System.NotImplementedException();
        }

        public Dictionary<string, SequenceLine> GetLines(){
            throw new System.NotImplementedException();
        }
    }
}