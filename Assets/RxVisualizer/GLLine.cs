using System;
using UnityEngine;

namespace RxVisualizer{
    public class GLLine{
        
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
        
        public static void Draw(Vector2 a, Vector2 b, Color color){
            GL.Begin(GL.LINES);
            GL.Color(color);
            GL.Vertex3(a.x, a.y, 0);
            GL.Vertex3(b.x, b.y, 0);
            GL.End();
        }
        
        public static void Draw(float ax, float ay, float bx, float by, Color color){
            GL.Begin(GL.LINES);
            GL.Color(color);
            GL.Vertex3(ax, ay, 0);
            GL.Vertex3(bx, by, 0);
            GL.End();
        }

        public static void PixelMatrixScope(Action action){
            
            CreateLineMaterial();
            // Apply the line material
            lineMaterial.SetPass(0);
            
            GL.PushMatrix();
            GL.LoadPixelMatrix();
            
            action();
            
            GL.PopMatrix();
        }
    }
}