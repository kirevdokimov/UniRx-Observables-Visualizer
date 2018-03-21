using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class TestScript : MonoBehaviour{

	
	private void Start(){
		Observable.ReturnUnit().Visualize("Pep");
	}
}
