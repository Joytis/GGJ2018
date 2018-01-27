using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Receiver : MonoBehaviour {

	public Color lightColor;
	public MeshRenderer _mr;

	// Use this for initialization
	void Awake () {
		_mr = GetComponent<MeshRenderer>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Light() {
		_mr.material.color = lightColor;
	}

}
