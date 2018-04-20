using System;
using System.Collections.Generic;
using RxVisualizer;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace RxVisualizer{

	public class RxVisualizerWindow : EditorWindow{
		
		private const string EditorprefZoom = "RxVisualizerWindow_zoomSlider";
		private const string EditorprefOriginX = "RxVisualizerWindow_origin_x";
		private const string EditorprefOriginY = "RxVisualizerWindow_origin_y";
		private static Vector2 DefaultOrigin{ get{ return Vector2.right * 200f + Vector2.up * 150f; } }
		private const int StartZoom = 50;
		
		public int ZoomSlider;

		private const float DistanceBetweenLines = 50f;

		private static Vector2 _origin = DefaultOrigin;

		[MenuItem("Window/Rx Visualizer")]
		private static void Init(){
			var window = GetWindow<RxVisualizerWindow>();
			window.Show();
			
		}

		private void OnGUI(){

			DrawLayout();

			Drawer.DrawLines(
				countOfLines: VisualizerItemHandler.CountOfContainers(),
				width: position.width,
				yAxisShift: _origin.y,
				distanceBetweenLines: DistanceBetweenLines);
			
			DrawItems();
			
			HandleMouseDrag();
		}

		private void DrawLayout(){
			EditorGUI.BeginChangeCheck();
			{
				ZoomSlider = EditorGUILayout.IntSlider("Zoom", ZoomSlider, 5, 50);
			}
			if (EditorGUI.EndChangeCheck()){
				ItemDrawer.WidthUnit = ZoomSlider;
			}

			if (GUILayout.Button("Clear")){
				Revert();
			}
		}
		
		/// Revert state to initial
		private static void Revert(){
			VisualizerItemHandler.Clear();
			_origin = DefaultOrigin;
		}

		private static void DrawItems(){	
			Rect? lastMouseEventRect = null;
			Item? lastMouseEventItem = null;
			
			// TODO Add containers ordering
			int layer = -1;
			
			foreach (var container in VisualizerItemHandler.Containers){	
				layer++;

				var labelPosition = Vector2.up * (_origin.y + DistanceBetweenLines * layer) + Vector2.right * (25f);
				
				Drawer.DrawLabel(labelPosition,container.GetName());
				
				var items = container.GetItems();
				var previousItem = new Item(){time = -1f};
				var previousPosition = Vector2.zero;
				foreach (var item in items){
					//Truncate data representation
					var itemText = item.data.Length > 4 ? "..." : item.data;

					var itemPosition = ItemDrawer.GetItemPosition(item.time, _origin, layer);
					
					// Stack items if it's close enough
					if (Mathf.Abs(previousItem.time - item.time) < 0.01f){
						itemPosition = previousPosition + Vector2.down * 5 + Vector2.right * 5;
					}
					
					previousItem = item;
					previousPosition = itemPosition;

					var rect = item.type == Item.Type.Next
						? ItemDrawer.DrawItemWithText(item, itemPosition, itemText)
						: ItemDrawer.DrawItem(item, itemPosition);

					// Mouse can touch a lot of items at once, so we keep in mind only last (overlaid) item
					if (!rect.Contains(Event.current.mousePosition)) continue;
					lastMouseEventRect = rect;
					lastMouseEventItem = item;

				}
			}
			
			if (lastMouseEventRect.HasValue){
				var item = lastMouseEventItem.Value;
				var rect = lastMouseEventRect.Value;
				var text = string.Format("Value : {0} \n Time : {1}", item.data, item.time);
				ItemDrawer.DrawItemBox(item, rect, text);
			}
		}

		private void HandleMouseDrag(){
			if (Event.current.type != EventType.MouseDrag) return;
			_origin += Event.current.delta;
			Event.current.Use();
			Repaint();
		}
		
		#region Life Cycle 

		private void OnInspectorUpdate(){
			Repaint();
		}
		
		private void OnFocus(){
			ItemDrawer.WidthUnit = ZoomSlider;
			var x = EditorPrefs.GetFloat(EditorprefOriginX, DefaultOrigin.x);
			var y = EditorPrefs.GetFloat(EditorprefOriginY, DefaultOrigin.y);
			_origin = new Vector2(x,y);
		}

		private void OnLostFocus(){
			EditorPrefs.SetFloat(EditorprefOriginX,_origin.x);
			EditorPrefs.SetFloat(EditorprefOriginY,_origin.y);
		}

		private void OnDisable(){
			EditorPrefs.SetInt(EditorprefZoom,ZoomSlider);
			EditorApplication.playModeStateChanged -= _onActionBeforeEnteringPlaymode;
		}
		
		private void OnEnable(){
			ZoomSlider = EditorPrefs.GetInt(EditorprefZoom, StartZoom);
			EditorApplication.playModeStateChanged -= _onActionBeforeEnteringPlaymode; // for sure
			EditorApplication.playModeStateChanged += _onActionBeforeEnteringPlaymode;
		}
		
		#endregion
		
		/// Clear window when entering to play mode
		private readonly Action<PlayModeStateChange> _onActionBeforeEnteringPlaymode = change => {
			if (change != PlayModeStateChange.ExitingEditMode) return;
			Revert();
		};

	}
}
