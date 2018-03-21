using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
		
		GUI.DrawTexture(new Rect(x,y,10,10), t);
		GUI.DrawTexture(new Rect(x+10,y,10,20), black);
		
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField("Text Field", myString);

		groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle("Toggle", myBool);
		x = EditorGUILayout.IntSlider("X", x, -50, 50);
		y = EditorGUILayout.IntSlider("Y", y, -50, 50);
		EditorGUILayout.EndToggleGroup();
		
		scrollPosition = GUI.BeginScrollView(new Rect(10, 300, 500, 100), scrollPosition, new Rect(0, 0, 220*x, 200));
		GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
		GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
		GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
		GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
		GUI.EndScrollView();
	}

	private void OnFocus(){
		Debug.Log("Focus");
		t = (Texture) EditorGUIUtility.Load ("Assets/Resources/greenCircle.png");
		black = (Texture) EditorGUIUtility.Load ("Assets/Resources/black.png");
	}
}
