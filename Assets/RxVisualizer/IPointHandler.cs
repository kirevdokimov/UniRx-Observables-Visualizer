using System.Collections.Generic;

namespace RxVisualizer{
    public interface IPointHandler{
        void Handle(string name, Point.PointType type, object data);
        Dictionary<string, SequenceLine> GetLines();
    }
}