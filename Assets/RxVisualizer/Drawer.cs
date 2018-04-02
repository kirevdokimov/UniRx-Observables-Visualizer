using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RxVisualizer{
    public static class Drawer{
        private static Texture2D[] mark = {
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/greenMark.png"),//0
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/redMark.png"),//1
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/blueMark.png"),//2
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/orangeMark.png"),//3
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/completedMark.png"),//4
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/errorMark.png"),//5
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/ohHiMark.png")//6
        };

        public enum Mark{
            green = 0, red = 1, blue = 2, orage = 3
        }

        private static Rect gridRect;
        private static float widthUnit; // количество расстояния в юнитах для одной секунды времени

        public static Rect DrawItem(Item item, int layer, string text){
            var rect = DrawItem(item, layer);
            
            if(item.type == Item.Type.next)
                GUI.Label(rect,text,new GUIStyle(){
                    normal = new GUIStyleState{
                        textColor = Color.black
                    },
                    alignment = TextAnchor.MiddleCenter
                });
            
            return rect;
        }

        public static Rect DrawItem(Item item, int layer){
            Texture2D drawmark;
            
            switch (item.type){
                case Item.Type.next : drawmark = mark[(int)item.mark]; break;
                case Item.Type.completed : drawmark = mark[4]; break;
                case Item.Type.error : drawmark = mark[5]; break;
                default : drawmark = mark[6]; break;
            }
            
            var origin = gridRect.position;
            var layerOffset = 50;
            
            var pointRect = new Rect(
                origin.x + widthUnit * item.time - drawmark.width/2,
                origin.y + layer * layerOffset - drawmark.height/2,
                drawmark.width,
                drawmark.height);   
            GUI.DrawTexture(pointRect,drawmark);
            return pointRect;
        }


        public static void DrawItemBox(Item i, Rect r){
            GUI.Box(new Rect(r.position,new Vector2(100,50)), "Hello");
        }

        private static GUIGrid.DrawConfig gridConfig = new GUIGrid.DrawConfig(){
            LargeLineColor = new Color(.55f,.55f,.55f,1f),
            SmallLineColor = new Color(.3f,.3f,.3f,1f),
            //UnitWidth = 250,
            //LargeWidthRatio = 1,
            UnitHeight = 50
        };

        public static void SetZoom(int value){
            widthUnit = value;
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