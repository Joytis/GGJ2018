using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalNode : MonoBehaviour {

	public PortalNode _link;

	// Use this for initialization
	void Awake () {
		if(!(_link != null && _link._link == this)) {
			Debug.LogError("No valid connection on " + gameObject);
		}
	}
}
