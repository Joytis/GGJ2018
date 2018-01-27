using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnColorOnReceive : MonoBehaviour {

	// Use this for initialization
	public Color color = Color.white;
	public List<MeshRenderer> thingsToColor;

	Dictionary<MeshRenderer, Color> originalColors = new Dictionary<MeshRenderer, Color>();

	// Find receiver in children and hook callbacks into it. 
	void Awake() {
		var rec = GetComponentInChildren<Receiver>();
		rec.RegisterDoSomething(new Receiver.DoSomething(TurnThingsColors));
		rec.RegisterAntiSomething(new Receiver.DoSomething(TurnColorsBack));

		foreach(var mesh in thingsToColor) {
			originalColors[mesh] = mesh.material.color;
		}
	}

	void TurnThingsColors() {
		foreach(var obj in thingsToColor) {
			var rend = obj.GetComponent<MeshRenderer>();
			rend.material.color = color;
		}
	}

	void TurnColorsBack() {
		foreach(var mesh in thingsToColor) {
			mesh.material.color = originalColors[mesh];
		}	
	}
}
