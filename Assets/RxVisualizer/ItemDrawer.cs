using UnityEditor;
using UnityEngine;

namespace RxVisualizer{
    public static class ItemDrawer{
        
        private static readonly Texture2D[] mark = {
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/greenMark.png"),//0
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/redMark.png"),//1
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/blueMark.png"),//2
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/orangeMark.png"),//3
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/checkmark.png"),//4
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/cross.png"),//5
        };
        // Расстояние в пикселях, для одной секунды пройденного времени с начала отсчета 
        public static int WidthUnit{ private get; set; }
        
        public static Rect DrawItemWithText(Item item, Vector2 position, string text){
            var rect = DrawItem(item, position);
            
            DrawCenteredText(rect, text);
            
            return rect;
        }
        
        public static Rect DrawItem(Item item, Vector2 position){
            
            var texture = GetTextureForItem(item);
            var rect = GetCenteredRectForTexture(position, texture); 
            
            GUI.DrawTexture(rect,texture);

            switch (item.type){
                case Item.Type.Completed:
                    GUI.DrawTexture(rect,mark[4]);
                    break;
                case Item.Type.Error:
                    GUI.DrawTexture(rect,mark[5]);
                    break;
            }

            return rect;
        }

        private static void DrawCenteredText(Rect rect, string text){
            GUI.Label(rect, text,
                new GUIStyle{normal = new GUIStyleState{textColor = Color.black}, alignment = TextAnchor.MiddleCenter});
        }

        private static Texture2D GetTextureForItem(Item item){
            return mark[(int) item.mark];
//            switch (item.type){
//                case Item.Type.Next : return mark[(int)item.mark];
//                case Item.Type.Completed : return mark[4];
//                case Item.Type.Error : return mark[5];
//                default : return mark[6];
//            }
        }
        
        private static Rect GetCenteredRectForTexture(Vector2 position, Texture texture){
            var textureSize = new Vector2(texture.width, texture.height);
            return new Rect(){
                position = position - textureSize*0.5f,
                size = textureSize
            };
        }

        public static void DrawItemBox(Item i, Rect itemRect, string content){
            var style = GUI.skin.box;
            style.normal.background = Texture2D.whiteTexture;

            GUI.Box(GetBoxRect(itemRect), new GUIContent(content) ,style);
        }

        private static Rect GetBoxRect(Rect itemRect){
            var labelHeight = 16;
            var size = new Vector2(180, labelHeight * 2);
            var xShift = Vector2.right * (itemRect.width - size.x) * 0.5f;
            var yShift = Vector2.up * itemRect.height * 1.5f;
            return new Rect(){size = size, position = itemRect.position + xShift + yShift};
        }

        #region GetItemPosition
        
        public static Vector2 GetItemPosition(float time, Vector2 originPosition, int layer){
            return originPosition + GetItemShiftByTime(time) + GetItemShiftByLayer(layer);
        }
        
        private static Vector2 GetItemShiftByLayer(int layer){
            const float layerOffset = 50;
            return Vector2.up * layer * layerOffset;
        }
        
        private static Vector2 GetItemShiftByTime(float time){
            return new Vector2{x = WidthUnit * time};
        }
        
        #endregion
        
        
    }
}