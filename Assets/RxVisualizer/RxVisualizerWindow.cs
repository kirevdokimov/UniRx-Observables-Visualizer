using System;
using System.Collections.Generic;
using RxVisualizer;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace RxVisualizer{



	public class RxVisualizerWindow : EditorWindow{
		
		private const string EDITORPREF_ZOOM = "RxVisualizerWindow_zoomSlider";
		private const string EDITORPREF_SCROLL = "RxVisualizerWindow_scrollValue";
		private const int startZoom = 50;

		[MenuItem("Window/Rx Visualizer")]
		private static void Init(){
			var window = GetWindow<RxVisualizerWindow>();
			window.Show();
		}

		private void OnFocus(){
			Drawer.SetZoom(zoomSlider);
			Drawer.scrollValue = EditorPrefs.GetFloat(EDITORPREF_SCROLL, 0);
		}

		private void OnLostFocus(){
			EditorPrefs.SetFloat(EDITORPREF_SCROLL,Drawer.scrollValue);
		}

		private void OnDisable(){
			EditorPrefs.SetInt(EDITORPREF_ZOOM,zoomSlider);
		}
		
		private void OnEnable(){
			zoomSlider = EditorPrefs.GetInt(EDITORPREF_ZOOM, startZoom);
		}

		public int zoomSlider;

		void OnGUI(){
			
			#region Draw Inspector
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
			var margRect = GetRectWithMargin(gridRect, 100, 25, 25, 25);
			Drawer.SetGridRect(margRect);
			Drawer.DrawGrid();
			#endregion
			
			#region Draw Items
			
			Rect? lastMouseEventRect = null;
			Item? lastMouseEventItem = null;
			// TODO Добавить порядок для контейнеров
			int layer = -1;
			foreach (var container in VisualizerItemHandler.Containers){
				layer++;
				
				
				Drawer.DrawLabel(container.GetName(),layer);
				
				var items = container.GetItems();
				foreach (var item in items){
					Rect rect;
					
					if (item.data.Length > 4){
						rect = Drawer.DrawItem(item, layer,"...");
						
					}else{
						rect = Drawer.DrawItem(item, layer,item.data);
					}
					
					if (rect.Contains(Event.current.mousePosition)){
						lastMouseEventRect = rect;
						lastMouseEventItem = item;
					}
				}
			}
			
			if (lastMouseEventRect.HasValue){
				Drawer.DrawItemBox(lastMouseEventItem.Value,lastMouseEventRect.Value);
			}
			
			#endregion
			
			if (Event.current.type == EventType.MouseDrag){
				Drawer.scrollValue += Event.current.delta.x;
				Event.current.Use();
				Repaint();
			}
			
		}

		private static Rect GetRectWithMargin(Rect r, int top, int bottom, int left, int right){
			r.Set(r.position.x + left, r.position.y + top, r.size.x - left - right, r.size.y - top - bottom);
			return r;
		}

		void OnInspectorUpdate(){
			Repaint();
		}

	}
}
