using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DefaultNamespace;
using RxVisualizer;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class RxVisualizerWindow : EditorWindow {

	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	int x = 5;
	int y = 5;

	private static Texture t;
	private static Texture black;

	private Vector2 scrollPosition;
	
	[MenuItem("Window/Rx Visualizer")]
	static void Init(){
		
		var window = GetWindow<RxVisualizerWindow>();
		window.Show();
	}

	public float timeToLength = 50; // 50 pixels per second

	void OnGUI(){
		x = EditorGUILayout.IntField("X", x);
		y = EditorGUILayout.IntField("Y", y);
		timeToLength = EditorGUILayout.FloatField("Len", timeToLength);

		DrawScrollView(DrawScrollViewContent);
	}

	void DrawScrollViewContent(Rect scrollViewContentRect){
		GUI.Button(new Rect(0, 0, 10, 10), "Top-left");
		GUI.Button(new Rect(scrollViewContentRect.width-10, 0, 10, 10), "Top-right");
		GUI.Button(new Rect(0, scrollViewContentRect.height-10, 10, 10), "Bottom-left");
		GUI.Button(new Rect(scrollViewContentRect.width-10, scrollViewContentRect.height-10, 10, 10), "Bottom-right");
		
		foreach (var sequenceLine in lines.Values){
			sequenceLine.OnGUI(timeToLength);
		}
	}

	void DrawScrollView(Action<Rect> content){
		var windowWidth = position.width;
		var windowHeight = position.height;
		var ScrollViewYAxisOffset = 100f;
		var ScrollViewRect = new Rect(0,ScrollViewYAxisOffset,windowWidth,windowHeight-ScrollViewYAxisOffset);
		var ScrollViewContentRect = new Rect(0,0,1000,500);
		scrollPosition = GUI.BeginScrollView(ScrollViewRect, scrollPosition, ScrollViewContentRect);

		content(ScrollViewContentRect);
		
		GUI.EndScrollView();
	}

	void OnInspectorUpdate(){Repaint();}


	public static void OnNext(object obj, string name){
		AddPoint(name, Point.PointType.next);
	}
	public static void OnError(Exception ex, string name){
		AddPoint(name, Point.PointType.error);
	}
	public static void OnCompleted(string name){
		AddPoint(name, Point.PointType.completed);
	}

	private static bool IsUnknownName(string name){
		return !lines.ContainsKey(name);
	}

	private static void AddPoint(string name, Point.PointType type){
		if (IsUnknownName(name)){
			var layer = lines.Count;
			lines.Add(name,new SequenceLine(name,layer));
			Debug.Log("New Line and point to "+name);
			lines[name].AddPoint(new Point{time = Time.time, type = type});
		} else {
			Debug.Log("New point to "+name);
			lines[name].AddPoint(new Point{time = Time.time, type = type});
		}
	}

	private static Dictionary<string, SequenceLine> lines = new Dictionary<string, SequenceLine>();

	private void OnFocus(){
		Debug.Log("Focus");
	}

	public static class VisualizerTextures{
		public static Texture line = (Texture) EditorGUIUtility.Load ("Assets/Resources/black.png");
		public static Texture point = (Texture) EditorGUIUtility.Load ("Assets/Resources/greenCircle.png");
		public static Texture completedIcon = (Texture) EditorGUIUtility.Load ("Assets/Resources/completedIcon.png");
		public static Texture errorIcon = (Texture) EditorGUIUtility.Load ("Assets/Resources/errorIcon.png");
	}


}
