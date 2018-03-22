using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Texture = RxVisualizerWindow.VisualizerTextures;

namespace RxVisualizer{
    public class SequenceLine{
        public string name;
        public Vector2 origin;
        public Vector2 size = new Vector2(500,1);
        private List<Point> points;

        public int layer;

        public SequenceLine(string name, int layer){
            this.name = name;
            points = new List<Point>();
            this.layer = layer;
            origin.y = 50 + layer * 70f;
        }

        public void OnGUI(float timeToLength){
            GUI.DrawTexture(new Rect(origin,size), Texture.line );

            foreach (var point in points){
                UnityEngine.Texture tx = GetTextureByType(point.type);
                var pointRect = new Rect(origin.x + point.time * timeToLength - tx.width/2, origin.y - tx.height/2, tx.width, tx.height);
                
                GUI.DrawTexture(pointRect,tx);
                
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

        public UnityEngine.Texture GetTextureByType(Point.PointType type){
            switch (type){
                case Point.PointType.error : return Texture.errorIcon;
                case Point.PointType.completed : return Texture.completedIcon;
                case Point.PointType.next :
                default : return Texture.point;
            }
        }
    }
}