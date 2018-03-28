using UnityEngine;
// runemarkstudio.com/extend-your-unity-editor-with-grid-background/
// docs.unity3d.com/ScriptReference/GL.html
public static class GUIGrid{

    public struct DrawConfig{
        
        public Color SmallLineColor;
        public Color LargeLineColor;

        public Vector2 SmallTileSize{
            get{ 
                _smallTileSize.x = Mathf.Max(5, _smallTileSize.x);
                _smallTileSize.y = Mathf.Max(5, _smallTileSize.y);
                return _smallTileSize;
                
            }
            set{
                _smallTileSize.x = Mathf.Max(5, value.x);
                _smallTileSize.y = Mathf.Max(5, value.y);
            }
        }

        public float Subdivisions{
            set{ LargeTileSize = SmallTileSize * Mathf.Max(1,value); }
        }

        public Vector2 LargeTileSize{ get; private set; }

        private Vector2 _smallTileSize;
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
        SmallTileSize = new Vector2(5, 5),
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
        for (float x = 0; x < rect.width; x += config.SmallTileSize.x){
            GL.Begin(GL.LINES);
            GL.Color((x % config.LargeTileSize.x == 0) ? config.LargeLineColor : config.SmallLineColor);
            GL.Vertex3(rect.x + x, rect.y, 0);
            GL.Vertex3(rect.x + x, rect.y + rect.height, 0);
            GL.End();
        }

        // Horizontal lines
        for (float y = 0; y < rect.height; y += config.SmallTileSize.y){
            GL.Begin(GL.LINES);
            GL.Color((y % config.LargeTileSize.y == 0) ? config.LargeLineColor : config.SmallLineColor);
            GL.Vertex3(rect.x, rect.y + y, 0);
            GL.Vertex3(rect.x + rect.width, rect.y + y, 0);
            GL.End();
        }

        GL.PopMatrix();
    }
}