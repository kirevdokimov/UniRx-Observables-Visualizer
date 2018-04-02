using System;
using UniRx;
using UnityEngine;
using RxVisualizer;

public class TestScript : MonoBehaviour{

	
	private void Start(){
		
		MarkMapper<long> mm = new MarkMapper<long>();
		mm.AddRule(i => i > 5, Drawer.Mark.blue);
		
		Observable.Interval(TimeSpan.FromSeconds(1f)).Visualize("Alpha",mm);
		Observable.Interval(TimeSpan.FromSeconds(1f)).Delay(TimeSpan.FromSeconds(.25f)).Visualize("Beta");
		Observable.Interval(TimeSpan.FromSeconds(1f)).Select(x => x+9997).Visualize("BetaOverflow");
		Observable.ReturnUnit().Delay(TimeSpan.FromSeconds(4.28f)).Visualize("Echo");
		Observable.Interval(TimeSpan.FromSeconds(1f)).Take(8).Visualize("Charlie");
		Observable.Throw<Exception>(new Exception("Ouch")).Visualize("ErrorHere");
	}
}
