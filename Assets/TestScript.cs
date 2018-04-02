using System;
using UniRx;
using UnityEngine;
using RxVisualizer;

public class TestScript : MonoBehaviour{

	
	private void Start(){
		// Маппер для long последовательности, где для каждого события со значением >5 будет рисоваться синяя метка
		MarkMapper<long> alphaMapper = new MarkMapper<long>();
		alphaMapper.AddRule(i => i > 5, Drawer.Mark.blue); 
		
		MarkMapper<long> betaMapper = new MarkMapper<long>();
		betaMapper.AddRule(i => i < 3, Drawer.Mark.blue); 
		betaMapper.AddRule(i => i == 0, Drawer.Mark.red); 
		betaMapper.AddRule(i => i == 6, Drawer.Mark.red); 
		
		Observable.Interval(TimeSpan.FromSeconds(1f)).Visualize("Alpha",alphaMapper);
		Observable.Interval(TimeSpan.FromSeconds(1f)).Delay(TimeSpan.FromSeconds(.25f)).Visualize("Beta",betaMapper);
		Observable.Interval(TimeSpan.FromSeconds(1f)).Select(x => x+9997).Visualize("BetaOverflow");
		Observable.ReturnUnit().Delay(TimeSpan.FromSeconds(4.28f)).Visualize("Echo");
		Observable.Interval(TimeSpan.FromSeconds(1f)).Take(8).Visualize("Charlie");
		Observable.Throw<Exception>(new Exception("Ouch")).Visualize("ErrorHere");
	}
}
