using System;
using System.Collections.Generic;
using RxVisualizer;
using UniRx;
using UnityEditor;
using UnityEngine;

public class RxVisualizerWindow : EditorWindow {

	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	int x = 5;
	int y = 5;
	private Vector2Int size;
	
	private static Texture t;
	private static Texture black;

	private Vector2 scrollPosition;
	
	[MenuItem("Window/Rx Visualizer")]
	static void Init(){
		
		var window = GetWindow<RxVisualizerWindow>();
		window.Show();
	}

	public int slider;

	void OnGUI(){
		EditorGUI.BeginChangeCheck();
		slider = EditorGUILayout.IntSlider("Slider", slider, 5, 50);
		if (EditorGUI.EndChangeCheck()){
			gridConfig.UnitWidth = slider;
		}

		DrawGrid();
		
		// TODO Добавить порядок для контейнеров
		int layer = -1;
		foreach (var container in VisualizerItemHandler.Containers){
			layer++;
			var items = container.GetItems();
			foreach (var item in items){
				DrawItem(item,layer);
				Debug.Log("Render");
			}
		}
	}

	private void DrawItem(Item item, int layer){
		var tx = VisualizerTextures.point;
		var pointRect = new Rect(0 + item.time * slider - tx.width/2, 100 + layer * 50 - tx.height/2, tx.width, tx.height);   
		GUI.DrawTexture(pointRect,tx);
	}

	private GUIGrid.DrawConfig gridConfig = new GUIGrid.DrawConfig(){
		LargeLineColor = new Color(.35f,.35f,.35f,1f),
		SmallLineColor = new Color(.3f,.3f,.3f,1f),
		UnitSize = new Vector2Int(5,5),
		Subdivisions = 5
	};

	void DrawGrid(){
		var windowWidth = position.width;
		var windowHeight = position.height;
		var gridRect = new Rect(0, 0, windowWidth, windowHeight);
		var margRect = GetRectWithMargin(gridRect, 100, 0, 0, 0);
		GUIGrid.Draw(margRect,gridConfig);
	}

	private Rect GetRectWithMargin(Rect r, int top, int bottom, int left, int right){
		return new Rect(r.position.x + left,r.position.y + top, r.size.x - left -right,r.size.y - top - bottom);
	}

	void OnInspectorUpdate(){Repaint();}

	private void OnFocus(){
		Debug.Log("Focus");
	}

	public static class VisualizerTextures{
		public static Texture line = (Texture) EditorGUIUtility.Load ("Assets/Resources/black.png");
		public static Texture point = (Texture) EditorGUIUtility.Load ("Assets/Resources/greenCircle.png");
		public static Texture completedIcon = (Texture) EditorGUIUtility.Load ("Assets/Resources/completedIcon.png");
		public static Texture errorIcon = (Texture) EditorGUIUtility.Load ("Assets/Resources/errorIcon.png");
	}

//	void DrawScrollViewContent(Rect scrollViewContentRect){
//		GUI.Button(new Rect(0, 0, 10, 10), "Top-left");
//		GUI.Button(new Rect(scrollViewContentRect.width-10, 0, 10, 10), "Top-right");
//		GUI.Button(new Rect(0, scrollViewContentRect.height-10, 10, 10), "Bottom-left");
//		GUI.Button(new Rect(scrollViewContentRect.width-10, scrollViewContentRect.height-10, 10, 10), "Bottom-right");
//		
//		Rect rect = new Rect(0, 0, 800, 600);
//
//		//Background
//		var _bgStyle = new GUIStyle();
//		//_bgStyle.normal.background = new Color(.36f, .36f, .36f);
//		GUI.Box(rect,"", _bgStyle);
//
//		
//		foreach (var sequenceLine in lines.Values){
//			sequenceLine.OnGUI(timeToLength);
//		}
//	}
//
//	void DrawScrollView(Action<Rect> content){
//		var windowWidth = position.width;
//		var windowHeight = position.height;
//		var ScrollViewYAxisOffset = 100f;
//		var ScrollViewRect = new Rect(0,ScrollViewYAxisOffset,windowWidth,windowHeight-ScrollViewYAxisOffset);
//		var ScrollViewContentRect = new Rect(0,0,1000,500);
//		scrollPosition = GUI.BeginScrollView(ScrollViewRect, scrollPosition, ScrollViewContentRect);
//
//		content(ScrollViewContentRect);
//		
//		GUI.EndScrollView();
//	}
}
