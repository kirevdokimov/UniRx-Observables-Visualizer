using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RxVisualizer{
    public class SequenceLine{
        public string name;
        public Vector2 origin;
        public Vector2 size = new Vector2(500,1);
        private List<Point> points;
        public static Texture lineTexture;
        public static Texture pointTexture;

        public int layer;

        public SequenceLine(string name, int layer){
            this.name = name;
            points = new List<Point>();
            this.layer = layer;
            origin.y = 50 + layer * 70f;
        }

        public void OnGUI(float timeToLength){
            GUI.DrawTexture(new Rect(origin,size), lineTexture );

            foreach (var point in points){
                var pointRect = new Rect(origin.x + point.time * timeToLength - 5, origin.y - 5, 10, 10);
                GUI.DrawTexture(pointRect,pointTexture );
                
                // Срабатывает при наведении на rect
                if (pointRect.Contains(Event.current.mousePosition)){
                    Debug.Log(point.time);
                }
            }

            var lineLabel = new GUIContent(name);
            Rect rt = GUILayoutUtility.GetRect(lineLabel, EditorStyles.label);
            rt.position = origin - Vector2.up*rt.height;
            GUI.Label(rt, lineLabel);
        }

        public void AddPoint(Point p){
            points.Add(p);
        }
    }
}