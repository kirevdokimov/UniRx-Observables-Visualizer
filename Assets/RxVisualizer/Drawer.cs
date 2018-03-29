using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace RxVisualizer{
    public static class Drawer{
        private static Texture2D point = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/greenCircle.png");
        
        public static Rect gridRect;

        public static Rect DrawItem(Item item, int layer){
            var origin = gridRect.position;
            var layerOffset = 50;
            // Тут важна зависимость от UnitWidth, чтобы небыло разных мер для отрисовки сетки и отрисовки меток.
            var unitsPerTime = gridConfig.UnitWidth; // количество расстояния в юнитах для одной секунды времени
            
            var pointRect = new Rect(
                origin.x + unitsPerTime * item.time - point.width/2,
                origin.y + layer * layerOffset - point.height/2,
                point.width,
                point.height);   
            GUI.DrawTexture(pointRect,point);
            return pointRect;
        }

        private static GUIGrid.DrawConfig gridConfig = new GUIGrid.DrawConfig(){
            LargeLineColor = new Color(.55f,.55f,.55f,1f),
            SmallLineColor = new Color(.3f,.3f,.3f,1f),
            UnitWidth = 16,
            LargeWidthRatio = 5,
            UnitHeight = 50
        };

        public static void SetZoom(int value){
            if (gridConfig.UnitWidth == 0) return;
            var ratio = gridConfig.LargeUnitWidth / gridConfig.UnitWidth;
            gridConfig.UnitWidth = value;
            gridConfig.LargeUnitWidth = ratio * value;
        }

        public static void DrawGrid(){
            if(gridRect.size == Vector2.zero) throw new NullReferenceException("call SetGridRect before DrawGrid");
            GUIGrid.Draw(gridRect,gridConfig);
        }

        public static void SetGridRect(Rect rect){
            gridRect = rect;
        }
    }
}