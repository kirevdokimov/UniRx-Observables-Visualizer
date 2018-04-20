using System;
using UniRx;
using UnityEngine;
using RxVisualizer;

public class TestScript : MonoBehaviour{

	
	private void Start(){
		
		var alpha = Observable.Interval(TimeSpan.FromSeconds(1f));
//		var beta = alpha.Take(5);
//		var gamma = alpha.Where(x => x > 2);
//		
//		MarkMapper<long> mm = new MarkMapper<long>();
//		
//		alpha.Visualize("Alpha",new MarkMapper<long>().Any(Item.Mark.Blue));
//		beta.Visualize("Beta",new MarkMapper<long>().Any(Item.Mark.Red));
//		gamma.Visualize("Gamma");
//		
//		var customObs = Observable.Create<char>(observer => {
//			observer.OnNext('A');
//			observer.OnNext('B');
//			observer.OnNext('C');
//			observer.OnCompleted();
//			return Disposable.Empty;
//		});
//		var customObsErr = Observable.Create<char>(observer => {
//			observer.OnNext('A');
//			observer.OnError(new Exception("Kernel panic"));
//			return Disposable.Empty;
//		});
//
//		customObs.Visualize("CustomSucc");
//		customObsErr.Visualize("CustomErr");


		alpha.Visualize("WithoutSubscription");
		alpha.Visualize("WithSubscription").Subscribe();
		var temp = alpha.Visualize("WithTemporarySubscription").Subscribe();
		Observable.Timer(TimeSpan.FromSeconds(8)).Subscribe(l => { temp.Dispose(); });
		
		var mult = alpha.Visualize("WithMultipleSubscription");
		mult.Subscribe();
		mult.Subscribe();
		mult.Subscribe();
		
	}
}
