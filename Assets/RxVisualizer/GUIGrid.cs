using UnityEngine;
// runemarkstudio.com/extend-your-unity-editor-with-grid-background/
// docs.unity3d.com/ScriptReference/GL.html
public static class GUIGrid{

    public struct DrawConfig{
        
        public Color SmallLineColor;
        public Color LargeLineColor;

        public Vector2Int UnitSize{
            set{
                UnitWidth = value.x;
                UnitHeight = value.y;
            }
        }
        public Vector2Int LargeUnitSize{
            set{
                LargeUnitWidth = value.x;
                LargeUnitHeight = value.y;
            }
        }

        public int Subdivisions{
            set{
                LargeUnitWidth = UnitWidth * value;
                LargeUnitHeight = UnitHeight * value;
            }
        }

        public int UnitWidth{
            get{ return _uw; }
            set{
                _uw = Mathf.Max(5, value);
                LargeUnitWidth = UnitWidth * value;
            }
        }

        public int UnitHeight{
            get{ return _uh; }
            set{
                _uh = Mathf.Max(5, value);
                LargeUnitHeight = UnitHeight * value;
            }
        }

        public int LargeUnitWidth{
            get{ return _luw; }
            private set{ _luw = Mathf.Max(5, value); }
        }

        public int LargeUnitHeight{
            get{ return _luh; }
            private set{ _luh = Mathf.Max(5, value); }
        }

        private int _uw;
        private int _uh;
        
        private int _luw;
        private int _luh;

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
        UnitSize = new Vector2Int(5, 5),
        Subdivisions = 5
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
        
        
        
        // Vertical lines
        for (var x = 0; x < rect.width; x += config.UnitWidth){
            GL.Begin(GL.LINES);
            GL.Color((x % config.LargeUnitWidth == 0) ? config.LargeLineColor : config.SmallLineColor);
            GL.Vertex3(rect.x + x, rect.y, 0);
            GL.Vertex3(rect.x + x, rect.y + rect.height, 0);
            GL.End();
        }

        // Horizontal lines
        for (var y = 0; y < rect.height; y += config.UnitHeight){
            GL.Begin(GL.LINES);
            GL.Color((y % config.LargeUnitHeight == 0) ? config.LargeLineColor : config.SmallLineColor);
            GL.Vertex3(rect.x, rect.y + y, 0);
            GL.Vertex3(rect.x + rect.width, rect.y + y, 0);
            GL.End();
        }

        GL.PopMatrix();
    }
}