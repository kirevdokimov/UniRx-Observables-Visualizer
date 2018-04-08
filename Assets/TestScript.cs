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
		
		alpha.Visualize("Alpha",new MarkMapper<long>().Any(Item.Mark.Blue));
		beta.Visualize("Beta",new MarkMapper<long>().Any(Item.Mark.Red));
		gamma.Visualize("Gamma");
		
		var customObs = Observable.Create<int>(observer => {
			observer.OnNext(3);
			observer.OnNext(2);
			observer.OnNext(1);
			observer.OnCompleted();
			return Disposable.Empty;
		});

		customObs.Visualize("Custom");
	}
}
