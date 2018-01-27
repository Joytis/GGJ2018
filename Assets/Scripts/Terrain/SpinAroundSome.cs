using System;
using UnityEngine;

public class SpinAroundSome : MonoBehaviour {

	public Vector3 spinRate;
	Quaternion spin;

	void Update() {
		spin = Quaternion.Euler(spinRate * Time.deltaTime);
		transform.rotation *= spin;
	}

}