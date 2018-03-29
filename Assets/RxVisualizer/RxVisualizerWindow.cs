using System;
using System.Collections.Generic;
using RxVisualizer;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace RxVisualizer{



	public class RxVisualizerWindow : EditorWindow{

		[MenuItem("Window/Rx Visualizer")]
		private static void Init(){
			var window = GetWindow<RxVisualizerWindow>();
			window.Show();
			
		}

		public int zoomSlider;

		void OnGUI(){

			EditorGUI.BeginChangeCheck();
			{
				zoomSlider = EditorGUILayout.IntSlider("Zoom", zoomSlider, 5, 50);
			}
			if (EditorGUI.EndChangeCheck()){
				Drawer.SetZoom(zoomSlider);
			}

			if (GUILayout.Button("Clear")){
				VisualizerItemHandler.Clear();
			}

			var windowWidth = position.width;
			var windowHeight = position.height;
			var gridRect = new Rect(0, 0, windowWidth, windowHeight);
			var margRect = GetRectWithMargin(gridRect, 100, 0, 0, 0);
			Drawer.DrawGrid(margRect);

			// TODO Добавить порядок для контейнеров
			int layer = -1;
			foreach (var container in VisualizerItemHandler.Containers){
				layer++;
				var items = container.GetItems();
				foreach (var item in items){
					var rect = Drawer.DrawItem(item, layer, zoomSlider);
					if (rect.Contains(Event.current.mousePosition)){
						GUI.Box(new Rect(Event.current.mousePosition,new Vector2(100,50)), "Hello");
					}
				}
			}
		}

		private static Rect GetRectWithMargin(Rect r, int top, int bottom, int left, int right){
			return new Rect(r.position.x + left, r.position.y + top, r.size.x - left - right, r.size.y - top - bottom);
		}

		void OnInspectorUpdate(){
			Repaint();
		}

	}
}
