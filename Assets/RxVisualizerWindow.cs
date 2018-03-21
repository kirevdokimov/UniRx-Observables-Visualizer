using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DefaultNamespace;
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

		var windowWidth = position.width;
		var windowHeight = position.height;
		var ScrollViewYAxisOffset = 100f;
		var ScrollViewRect = new Rect(0,ScrollViewYAxisOffset,windowWidth,windowHeight-ScrollViewYAxisOffset);
		var ScrollViewContentRect = new Rect(0,0,1000,500);
		scrollPosition = GUI.BeginScrollView(ScrollViewRect, scrollPosition, ScrollViewContentRect);
		
		GUI.Button(new Rect(0, 0, 10, 10), "Top-left");
		GUI.Button(new Rect(ScrollViewContentRect.width-10, 0, 10, 10), "Top-right");
		GUI.Button(new Rect(0, ScrollViewContentRect.height-10, 10, 10), "Bottom-left");
		GUI.Button(new Rect(ScrollViewContentRect.width-10, ScrollViewContentRect.height-10, 10, 10), "Bottom-right");
		
		DrawLine(50,50,ScrollViewContentRect.width-100f);
		
		foreach (var point in points){
			DrawPoint(50,50,point);
			Repaint();
		}
		
		GUI.EndScrollView();
		
	}

	private static List<Point> points = new List<Point>();

	public struct Point{
		public float time;
	}

	public static void OnNext(object obj, string name){
		Debug.Log(name);
		points.Add(new Point{time = Time.time});
	}
	public static void OnError(Exception ex, string name){
		Debug.Log(ex);
	}
	public static void OnCompleted(string name){
		Debug.Log("cmpl "+name);
	}

	private void OnFocus(){
		Debug.Log("Focus");
		t = (Texture) EditorGUIUtility.Load ("Assets/Resources/greenCircle.png");
		black = (Texture) EditorGUIUtility.Load ("Assets/Resources/black.png");
	}

	private void DrawLine(float x, float y, float length){
		GUI.DrawTexture(new Rect(x,y,length,1f), black );
	}

	private void DrawPoint(float lineStartX, float lineStartY, Point p){
		GUI.DrawTexture(new Rect(lineStartX + p.time*timeToLength -5,lineStartY -5,10,10),t );
	}


}
