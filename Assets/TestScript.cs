using System;
using UniRx;
using UnityEngine;
using RxVisualizer;

public class TestScript : MonoBehaviour{

	
	private void Start(){
		
		MarkMapper<long> mm = new MarkMapper<long>();
		
		var alpha = Observable.Interval(TimeSpan.FromSeconds(1f));
		var beta = alpha.Take(5);
		var gamma = alpha.Where(x => x > 2);
		
		alpha.Visualize("Alpha",new MarkMapper<long>().Any(Drawer.Mark.blue));
		beta.Visualize("Beta",new MarkMapper<long>().Any(Drawer.Mark.red));
		gamma.Visualize("Gamma");
	}
}
