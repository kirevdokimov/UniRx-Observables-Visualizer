using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class TestScript : MonoBehaviour{

	
	private void Start(){
		//Observable.Interval(TimeSpan.FromSeconds(1f)).Visualize("pep");
		this.UpdateAsObservable().Visualize("l").Take(500);
	}
}
