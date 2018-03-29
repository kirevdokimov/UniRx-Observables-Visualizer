using UnityEditor;
using UnityEngine;

namespace RxVisualizer{
    public static class Drawer{

        private static Texture2D point = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/greenCircle.png");

        public static Rect DrawItem(Item item, int layer, int zoom){
            var pointRect = new Rect(0 + item.time * zoom - point.width/2, 100 + layer * 50 - point.height/2, point.width, point.height);   
            GUI.DrawTexture(pointRect,point);
            return pointRect;
        }

        private static GUIGrid.DrawConfig gridConfig = new GUIGrid.DrawConfig(){
            LargeLineColor = new Color(.35f,.35f,.35f,1f),
            SmallLineColor = new Color(.3f,.3f,.3f,1f),
            UnitSize = new Vector2Int(5,5),
            Subdivisions = 5
        };

        public static void SetZoom(int zoom){
            gridConfig.UnitWidth = Mathf.Max(5, zoom);
        }

        public static void DrawGrid(Rect rect){
            GUIGrid.Draw(rect,gridConfig);
        }
    }
}