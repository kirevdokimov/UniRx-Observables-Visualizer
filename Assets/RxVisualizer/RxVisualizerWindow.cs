using System;
using System.Collections.Generic;
using RxVisualizer;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace RxVisualizer{

	public class RxVisualizerWindow : EditorWindow{
		
		private const string EDITORPREF_ZOOM = "RxVisualizerWindow_zoomSlider";
		private const string EDITORPREF_ORIGIN_X = "RxVisualizerWindow_origin_x";
		private const string EDITORPREF_ORIGIN_Y = "RxVisualizerWindow_origin_y";
		private const int startZoom = 50;
		
		public int zoomSlider;

		private const float DistanceBetweenLines = 50f;

		private Vector2 _start;
		private Vector2 _shift;

		private Vector2 Origin = Vector2.right * 50f + Vector2.down * 150;

		[MenuItem("Window/Rx Visualizer")]
		private static void Init(){
			var window = GetWindow<RxVisualizerWindow>();
			window.Show();
			
		}
		
		void OnGUI(){

			DrawLayout();

			Drawer.DrawLines(
				countOfLines: VisualizerItemHandler.CountOfContainers(),
				width: position.width,
				yAxisShift: Origin.y,
				distanceBetweenLines: DistanceBetweenLines);
			
			DrawItems();
			
			HandleMouseDrag();
		}

		private void DrawLayout(){
			EditorGUI.BeginChangeCheck();
			{
				zoomSlider = EditorGUILayout.IntSlider("Zoom", zoomSlider, 5, 50);
			}
			if (EditorGUI.EndChangeCheck()){
				ItemDrawer.WidthUnit = zoomSlider;
			}

			if (GUILayout.Button("Clear")){
				VisualizerItemHandler.Clear();
			}
		}

		

		private void DrawItems(){
			
			Rect? lastMouseEventRect = null;
			Item? lastMouseEventItem = null;
			// TODO Добавить порядок для контейнеров
			int layer = -1;
			foreach (var container in VisualizerItemHandler.Containers){
				
				layer++;

				var labelPosition = Vector2.up * (Origin.y + DistanceBetweenLines * layer) + Vector2.right * (25f);
				
				Drawer.DrawLabel(labelPosition,container.GetName());
				
				var items = container.GetItems();
				Item previousItem = new Item(){time = -1f};
				Vector2 previousPosition = Vector2.zero;
				foreach (var item in items){
					
					var itemText = item.data.Length > 4 ? "..." : item.data;

					var itemPosition = ItemDrawer.GetItemPosition(item.time, Origin, layer);
					
					if (Mathf.Abs(previousItem.time - item.time) < 0.01f){
						itemPosition = previousPosition + Vector2.down * 5 + Vector2.right * 5;
						//Debug.Log("Shift item "+item.data);
					}
					previousItem = item;
					previousPosition = itemPosition;

					Rect rect;
					if(item.type  == Item.Type.Next)
						rect = ItemDrawer.DrawItemWithText(item, itemPosition, itemText);
					else{
						rect = ItemDrawer.DrawItem(item, itemPosition);
					}

					// Мышь может касаться сразу нескольких item'ов, поэтому мы запоминаем только последний
					if (rect.Contains(Event.current.mousePosition)){
						lastMouseEventRect = rect;
						lastMouseEventItem = item;
					}

				}
			}
			
			if (lastMouseEventRect.HasValue){
				var item = lastMouseEventItem.Value;
				var rect = lastMouseEventRect.Value;
				var text = string.Format("Value : {0} \n Time : {1}", item.data, item.time);
				ItemDrawer.DrawItemBox(item, rect, text);
			}
		}

		void HandleMouseDrag(){
			if (Event.current.type != EventType.MouseDrag) return;
			Origin += Event.current.delta;
			Event.current.Use();
			Repaint();
		}
		
		private static Rect GetRectWithMargin(Rect r, int top, int bottom, int left, int right){
			r.Set(r.position.x + left, r.position.y + top, r.size.x - left - right, r.size.y - top - bottom);
			return r;
		}
		
		#region Life Cycle 

		void OnInspectorUpdate(){
			Repaint();
		}
		
		private void OnFocus(){
			ItemDrawer.WidthUnit = zoomSlider;
			var x = EditorPrefs.GetFloat(EDITORPREF_ORIGIN_X, 0);
			var y = EditorPrefs.GetFloat(EDITORPREF_ORIGIN_Y, 0);
			Origin = new Vector2(x,y);
		}

		private void OnLostFocus(){
			EditorPrefs.SetFloat(EDITORPREF_ORIGIN_X,Origin.x);
			EditorPrefs.SetFloat(EDITORPREF_ORIGIN_Y,Origin.y);
		}

		private void OnDisable(){
			EditorPrefs.SetInt(EDITORPREF_ZOOM,zoomSlider);
		}
		
		private void OnEnable(){
			zoomSlider = EditorPrefs.GetInt(EDITORPREF_ZOOM, startZoom);
			
		}
		
		#endregion

	}
}
