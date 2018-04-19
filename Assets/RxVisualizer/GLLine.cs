using System;
using UnityEngine;

namespace RxVisualizer{
    /// <summary>
    /// Draws lines directly by graphic library (GL)
    /// </summary>
    public static class GLLine{
        private static Material _lineMaterial;

        private static void CreateLineMaterial(){
            if (_lineMaterial) return;
            
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            var shader = Shader.Find("Hidden/Internal-Colored");
            _lineMaterial = new Material(shader){hideFlags = HideFlags.HideAndDontSave};
            // Turn on alpha blending
            _lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            _lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            _lineMaterial.SetInt("_ZWrite", 0);
        }
        
//        public static void Draw(Vector2 a, Vector2 b, Color color){
//            GL.Begin(GL.LINES);
//            GL.Color(color);
//            GL.Vertex3(a.x, a.y, 0);
//            GL.Vertex3(b.x, b.y, 0);
//            GL.End();
//        }
        
        public static void Draw(float ax, float ay, float bx, float by, Color color){
            GL.Begin(GL.LINES);
            GL.Color(color);
            GL.Vertex3(ax, ay, 0);
            GL.Vertex3(bx, by, 0);
            GL.End();
        }
        /// <summary>
        /// Setting up enviroment for drawing
        /// </summary>
        /// <param name="action">Drawing action</param>
        public static void PixelMatrixScope(Action action){
            
            CreateLineMaterial();
            // Apply the line material
            _lineMaterial.SetPass(0);
            
            GL.PushMatrix();
            GL.LoadPixelMatrix();
            
            action();
            
            GL.PopMatrix();
        }
    }
}