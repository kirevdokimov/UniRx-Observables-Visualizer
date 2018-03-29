using UnityEditor;
using UnityEngine;

namespace RxVisualizer{
    public static class Drawer{

        private static Texture2D point = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/greenCircle.png");

        public static Rect DrawItem(Item item, int layer, int zoom){
            var pointRect = new Rect(0 + gridConfig.LargeUnitWidth * item.time - point.width/2, 100 + layer * 50 - point.height/2, point.width, point.height);   
            GUI.DrawTexture(pointRect,point);
            return pointRect;
        }

        private static GUIGrid.DrawConfig gridConfig = new GUIGrid.DrawConfig(){
            LargeLineColor = new Color(.55f,.55f,.55f,1f),
            //LargeLineColor = Color.blue,
            SmallLineColor = new Color(.3f,.3f,.3f,1f),
            //UnitHeight = 16,
            UnitWidth = 16,
            LargeWidthRatio = 5,
            LargeHeightRatio = 5
        };

        public static void SetZoom(int zoom){
            var ratio = gridConfig.LargeUnitWidth / gridConfig.UnitWidth;
            gridConfig.UnitWidth = zoom;
            gridConfig.LargeUnitWidth = ratio * zoom;
        }

        public static void DrawGrid(Rect rect){
            GUIGrid.Draw(rect,gridConfig);
        }
    }
}