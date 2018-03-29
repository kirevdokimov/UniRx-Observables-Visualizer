﻿using System;
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

		public int zoomSlider = 16;

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
			var margRect = GetRectWithMargin(gridRect, 100, 25, 25, 25);
			Drawer.SetGridRect(margRect);
			Drawer.DrawGrid();

			// TODO Добавить порядок для контейнеров
			int layer = -1;
			foreach (var container in VisualizerItemHandler.Containers){
				layer++;
				var items = container.GetItems();
				foreach (var item in items){
					var rect = Drawer.DrawItem(item, layer);
					if (rect.Contains(Event.current.mousePosition)){
						GUI.Box(new Rect(Event.current.mousePosition,new Vector2(100,50)), "Hello");
					}
				}
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
