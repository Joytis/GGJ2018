using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountdownTimer : MonoBehaviour {

	public float startTime = 60f; // in seconds. 
	public Receiver _recv; // We'll register a callback with this thing to stop the timer!

	float currentTime;

	int minutes;
	int seconds;

	enum State {
		Ticking,
		NotTicking
	};
	State currentState = State.Ticking;
	Text _tex;

	// Use this for initialization
	void Start() {
		currentTime = startTime;
		_tex = GetComponent<Text>();
		_recv.RegisterDoSomething(new Receiver.DoSomething(StopTimer));
		_recv.RegisterAntiSomething(new Receiver.DoSomething(StartTimer));
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState) {
			case State.Ticking:
				if(currentTime < 0)  {
					// Do some bad shit.
					currentState = State.NotTicking; 	
					break; // don't decrement out of FEAR. 
				}
				currentTime -= Time.deltaTime;
				break;
			case State.NotTicking:

				break;
		}

		// Translate time to text. 
		minutes = Mathf.FloorToInt(currentTime / 60f);
		seconds = Mathf.FloorToInt(currentTime % 60f);

		_tex.text = String.Format("{0}:{1:00}", minutes, seconds);
	}

	void StopTimer() {
		currentState = State.NotTicking; 	
	}
	void StartTimer() {
		currentState = State.Ticking; 	
	}
}
