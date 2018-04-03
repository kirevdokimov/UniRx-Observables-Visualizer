using System.Security.Cryptography.X509Certificates;
using RxVisualizer;
using UnityEngine;
// runemarkstudio.com/extend-your-unity-editor-with-grid-background/
// docs.unity3d.com/ScriptReference/GL.html
public static class GUIGrid{

    public struct DrawConfig{
        
        private const int MinValidValue = 5;
        
        public Color SmallLineColor;
        public Color LargeLineColor;

        public int UnitWidth;
        public int UnitHeight;

        public int LargeUnitWidth;
        public int LargeUnitHeight;

        public bool DrawHorizontal{
            get{ return UnitHeight >= MinValidValue; }
        }
        
        public bool DrawVertical{
            get{ return UnitWidth >= MinValidValue; }
        }
        
        public bool DrawLargeHorizontal{
            get{ return LargeUnitHeight >= MinValidValue; }
        }
        public bool DrawLargeVertical{
            get{ return LargeUnitWidth >= MinValidValue; }
        }

        public int LargeWidthRatio{
            set{ LargeUnitWidth = UnitWidth * value; }
        }
        public int LargeHeightRatio{
            set{ LargeUnitHeight = UnitHeight * value; }
        }
    }
    
    private static readonly DrawConfig DefaultConfig = new DrawConfig(){
        SmallLineColor = Color.blue,
        LargeLineColor = Color.red,
        UnitWidth = 5,
        UnitHeight = 5,
        LargeUnitWidth = 10
    };

    public static void Draw(Rect rect){
        Draw(rect,DefaultConfig);
    }

    public static void Draw(Rect rect, DrawConfig config){
        if (rect.width < 1 || rect.height < 1) return;
        
        GLLine.PixelMatrixScope(() => {
            
            if (config.DrawVertical){
                for (var x = 0; x < rect.width; x += config.UnitWidth){
                    var color = (config.DrawLargeVertical && x % config.LargeUnitWidth == 0) ? config.LargeLineColor : config.SmallLineColor;
                    GLLine.Draw(
                        rect.x + x, rect.y,
                        rect.x + x, rect.y + rect.height,
                        color);
                }
                
            }
            
            if (config.DrawHorizontal){
                for (var y = 0; y < rect.height; y += config.UnitHeight){
                    var color = (config.DrawLargeHorizontal && y % config.LargeUnitHeight == 0) ? config.LargeLineColor : config.SmallLineColor;
                    GLLine.Draw(
                        rect.x, rect.y + y,
                        rect.x + rect.width, rect.y + y,
                        color);
                }
            }
            
        });
    }
}