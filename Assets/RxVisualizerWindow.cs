using System.Collections;
using System.Collections.Generic;
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

	void OnGUI(){
		
//		GUI.DrawTexture(new Rect(x,y,10,10), t);
//		GUI.DrawTexture(new Rect(x+10,y,10,20), black);
		
//		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
//		myString = EditorGUILayout.TextField("Text Field", myString);
//
//		groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
//		myBool = EditorGUILayout.Toggle("Toggle", myBool);
		x = EditorGUILayout.IntField("X", x);
		y = EditorGUILayout.IntField("Y", y);
//		EditorGUILayout.EndToggleGroup();
		
		//scrollPosition = GUI.BeginScrollView(ScrollViewRect, scrollPosition, ScrollViewContentRect);

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
		GUI.EndScrollView();
		
	}

	private void OnFocus(){
		Debug.Log("Focus");
		t = (Texture) EditorGUIUtility.Load ("Assets/Resources/greenCircle.png");
		black = (Texture) EditorGUIUtility.Load ("Assets/Resources/black.png");
	}

	private void DrawLine(float x, float y, float length){
		GUI.DrawTexture(new Rect(x,y,length,1f), black );
	}


}
