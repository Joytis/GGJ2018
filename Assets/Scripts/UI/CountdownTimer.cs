using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountdownTimer : MonoBehaviour {

	public float startTime = 60f; // in seconds. 
	public Receiver _recv; // We'll register a callback with this thing to stop the timer!

	float currentTime;

	int minutes;
	int seconds;
	int miliseconds;

	enum State {
		Ticking,
		NotTicking
	};
	State currentState = State.Ticking;
	TextMeshProUGUI _tex;

	// Use this for initialization
	void Start() {
		// _tex.color = Color.red;

		currentTime = startTime;
		_tex = GetComponent<TextMeshProUGUI>();
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
		if(minutes < 0) minutes = 0;
		seconds = Mathf.FloorToInt(currentTime % 60f);
		if(seconds < 0) seconds = 0;
		miliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);
		if(miliseconds < 0) miliseconds = 0;

		_tex.text = String.Format("{0}:{1:00}.{2:000}", minutes, seconds, miliseconds);
	}

	void StopTimer() {
		currentState = State.NotTicking; 	
	}
	void StartTimer() {
		currentState = State.Ticking; 	
	}
}
