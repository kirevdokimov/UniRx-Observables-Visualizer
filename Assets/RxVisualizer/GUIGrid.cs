using System.Security.Cryptography.X509Certificates;
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

    static Material lineMaterial;
    static void CreateLineMaterial(){
        if (!lineMaterial){
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader){hideFlags = HideFlags.HideAndDontSave};
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
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

        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        GL.LoadPixelMatrix();
        
        
        
        if (config.DrawVertical){
            for (var x = 0; x < rect.width; x += config.UnitWidth){
                GL.Begin(GL.LINES);
                if (config.DrawLargeVertical)
                    GL.Color((x % config.LargeUnitWidth == 0) ? config.LargeLineColor : config.SmallLineColor);
                else
                    GL.Color(config.SmallLineColor);
                GL.Vertex3(rect.x + x, rect.y, 0);
                GL.Vertex3(rect.x + x, rect.y + rect.height, 0);
                GL.End();
            }
        }

        if (config.DrawHorizontal){
            for (var y = 0; y < rect.height; y += config.UnitHeight){
                GL.Begin(GL.LINES);
                if (config.DrawLargeHorizontal)
                    GL.Color((y % config.LargeUnitHeight == 0) ? config.LargeLineColor : config.SmallLineColor);
                else
                    GL.Color(config.SmallLineColor);
                GL.Vertex3(rect.x, rect.y + y, 0);
                GL.Vertex3(rect.x + rect.width, rect.y + y, 0);
                GL.End();
            }
        }

        GL.PopMatrix();
    }
}