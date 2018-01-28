using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Receiver))]
public class DoctorWinTheGame : MonoBehaviour {

	public float timeToWin = 3.0f;
	float currentTime;
	float currentIndex;
	public float Ratio { get{return currentIndex;} }
	Receiver _recv;

	enum State {
		Charging,
		Not
	}
	State currentState = State.Not;

	void Awake() {
		currentTime = 0;
		_recv = GetComponent<Receiver>();
		_recv.RegisterDoSomething(new Receiver.DoSomething(TurnOn));
		_recv.RegisterAntiSomething(new Receiver.DoSomething(TurnOff));
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState) {
			case State.Charging: 
				currentTime += Time.deltaTime;
				if(currentTime > timeToWin) {
					Debug.Log("HEY LOOK WE WON!");
					FindObjectOfType<GameController>().FinishLevel();
				}
				break;
			case State.Not: 
				currentTime -= Time.deltaTime;
				if(currentTime < 0) {
					currentTime = 0;
				}
				break;
		}
		Debug.Log("Time: " + currentTime);
		currentIndex = currentTime / timeToWin;
	}

	void TurnOn() {
		currentState = State.Charging;		
	}

	void TurnOff() {
		currentState = State.Not;
	}
}
