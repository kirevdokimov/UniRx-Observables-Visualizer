using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RxVisualizer{
    public static class Drawer{
        
        private static readonly Vector2 labelOffset = Vector2.down * 16;

        private static Vector2 GetShiftByLayer(int layer){
            const float layerOffset = 50;
            return Vector2.up * layer * layerOffset;
        }

        private static Vector2 GetLabelPosition(Vector2 originPosition, int layer){
            return originPosition + GetShiftByLayer(layer) + labelOffset;
        }

        private static Rect GetLabelRect(Vector2 position){
            return new Rect(position, new Vector2(400, 16));
        }

        public static void DrawLabel(Vector2 originPosition, int layer, string text){
            var pos = GetLabelPosition(originPosition, layer);
            var rect = GetLabelRect(pos);
            GUI.Label(rect, text);
        }

        private static GUIGrid.DrawConfig gridConfig = new GUIGrid.DrawConfig(){
            LargeLineColor = new Color(.55f,.55f,.55f,1f),
            SmallLineColor = new Color(.3f,.3f,.3f,1f),
            UnitHeight = 50
        };

        public static void SetZoom(int value){
            if (gridConfig.UnitWidth == 0) return;
            var ratio = gridConfig.LargeUnitWidth / gridConfig.UnitWidth;
            gridConfig.UnitWidth = value;
            gridConfig.LargeUnitWidth = ratio * value;
        }

        public static void DrawGrid(Rect gridRect){
            if(gridRect.size == Vector2.zero) throw new NullReferenceException("call SetGridRect before DrawGrid");
            GUIGrid.Draw(gridRect,gridConfig);
        }

        
    }
}