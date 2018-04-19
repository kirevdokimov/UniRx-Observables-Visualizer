using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RxVisualizer{
    /// <summary>
    /// The responsibility of the Drawer is to draw everything (Item marks, line labels, grid) for the RxVisualizerWindow
    /// </summary>
    public static class Drawer{

        private static readonly Vector2 LabelRectSize = new Vector2(400, 16); // Seems like 400x16 is good enough.

        /// Shifts position judging by layer of line which contains the label
        private static Vector2 GetShiftByLayer(int layer){
            const float layerOffset = 50; // I just think 50 is pretty good distance between lines
            return Vector2.up * layer * layerOffset;
        }

        /// <summary>
        /// Returns the position of label for line which is situated in [layer].
        /// </summary>
        /// <param name="originPosition">Origin required for using returned value for direct drawing</param>
        /// <param name="layer">Layer of line which contains the label</param>
        /// <returns></returns>
        private static Vector2 GetLabelPosition(Vector2 originPosition, int layer){
            var labelOffset = Vector2.down * 16; // Shifts position of label 16 px up (V2.down = aclually up because GUI Y axis look down)
            return originPosition + GetShiftByLayer(layer) + labelOffset;
        }

        private static Rect GetLabelRect(Vector2 position){
            return new Rect(position, LabelRectSize);
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
        
        /// <summary>
        /// Changes stretch of timeline
        /// </summary>
        /// <param name="value">Distance in pixels for 1 second range of timeline</param>
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