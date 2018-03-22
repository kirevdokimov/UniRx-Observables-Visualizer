using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class TestScript : MonoBehaviour{

	
	private void Start(){
		Observable.Interval(TimeSpan.FromSeconds(1f)).Visualize("Alpha");
		Observable.Interval(TimeSpan.FromSeconds(1f)).Delay(TimeSpan.FromSeconds(.25f)).Visualize("Beta");
		Observable.ReturnUnit().Delay(TimeSpan.FromSeconds(4.28f)).Visualize("Echo");
	}
}
