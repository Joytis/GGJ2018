using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour {

	public delegate void DoSomething();
	DoSomething internalSomething;
	DoSomething internalAntiSomething;
	bool triggered = false;

	public float timeout = 3.0f;
	float currentTime = 0f;

	void Update() {
		currentTime += Time.deltaTime;
		if(triggered && currentTime >= timeout) {
			internalAntiSomething();
			triggered = false;
		}
	}

	public void DoThing() {
		if(!triggered) {
			internalSomething(); // Dispatch delegate
			triggered = true;
			currentTime = 0f;
		}
	}


	// Registers a DoSomethingAction
	public void RegisterDoSomething(DoSomething something) {
		internalSomething += something;
	}

	// Registers the negation of the DoSomething action
	public void RegisterAntiSomething(DoSomething something) {
		internalAntiSomething += something;
	}
}
