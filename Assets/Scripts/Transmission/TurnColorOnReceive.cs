using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnColorOnReceive : MonoBehaviour {

	// Use this for initialization
	public Color color = Color.white;
	public List<MeshRenderer> thingsToColor;

	Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>();

	// Find receiver in children and hook callbacks into it. 
	void Awake() {
		var rec = GetComponentInChildren<Receiver>();
		rec.RegisterDoSomething(new Receiver.DoSomething(TurnThingsColors));
		rec.RegisterAntiSomething(new Receiver.DoSomething(TurnColorsBack));

        foreach (var mesh in thingsToColor)
            foreach (var mat in mesh.materials) {
                if (!mat.HasProperty("_Color")) continue;
                originalColors[mat] = mat.color;
            }
	}

	void TurnThingsColors() {
		foreach(var mesh in thingsToColor) 
			foreach(var mat in mesh.materials) 
				mat.color = color;
	}

	void TurnColorsBack() {	
		foreach(var mesh in thingsToColor) 
			foreach(var mat in mesh.materials) 
				mat.color = originalColors[mat];
	}
}
