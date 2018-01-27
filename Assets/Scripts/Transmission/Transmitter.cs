using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO(clark): Have reflector nodes that have reflcetion strength. 

[RequireComponent(typeof(LineRenderer))]
public class Transmitter : MonoBehaviour {

	public float length = 50;
	public LayerMask collisions;
	public LayerMask terminalCollisions;
	public int maxBounceCount = 20;

	HashSet<int> terminalLayers = new HashSet<int>();

	int currentCount = 0;

	List<Vector3> positions = new List<Vector3>();
	// Keep a set of beam contacts we've run into recently. 
	Dictionary<IBeamContact, RaycastHit> prevContacts = new Dictionary<IBeamContact, RaycastHit>();
	Dictionary<IBeamContact, RaycastHit> newContacts = new Dictionary<IBeamContact, RaycastHit>();

	LineRenderer _lr;

	void Awake()  {
		_lr = GetComponent<LineRenderer>();

		// Check for bitshifted layers, add them to terminal layers. 
		for(int i = 0; i < 32; i++) 
			if(((terminalCollisions >> i) & 0x1) == 1) 
				terminalLayers.Add(i);
	}

	// Update is called once per frame
	void Update () {

		positions.Clear();

		// Garbage collected and everything! 
		prevContacts = newContacts;
		newContacts = new Dictionary<IBeamContact, RaycastHit>();

		Vector3 startPosition = transform.position;
		Vector3 direction = transform.forward;
		
		RaycastHit hit;
		currentCount = 0;

		bool terminate = false;

		// Continue shooting out rays until things don't get hit. 
		while(currentCount < maxBounceCount &&
			  Physics.Raycast(startPosition, direction, out hit, length, collisions) ) {

			positions.Add(startPosition);
			startPosition = hit.point;
			direction = Vector3.Reflect(direction, hit.normal);

			currentCount++;

			// Check for special collisions()
			var go = hit.collider.gameObject;

			// Check if we have a beam Contact on the thing. If we do, sick!
			var bc = go.GetComponent<IBeamContact>();
			if(bc != null) {
				// Activate the stuff. 
				newContacts[bc] = hit; // Don't care if it's already there. C# has our back here. 
			}

			// DON"T DO ANTHING MORE IF OBSTRUCTABLE
			//=============================================
			if(terminalLayers.Contains(go.layer)) {
				terminate = true;
				break;
			}

			if(go.layer == LayerMask.NameToLayer("Receiver")) {
				var rec = go.GetComponent<Receiver>();
				if(rec == null) {
					rec = go.GetComponentInChildren<Receiver>();
				}
				rec.DoThing();
				terminate = true;
			}

		}

		// Debug.Log("PREVIOUS CONTACTS");
		// foreach(var bc in prevContacts) 
			// Debug.Log(bc);
		// Debug.Log("NEW CONTACTS");
		// foreach(var bc in newContacts) 
			// Debug.Log(bc);


		// Deactivate things that aren't contacted anymore. 
		var notInContacts = prevContacts.Keys.Where(bc => !newContacts.ContainsKey(bc));
		foreach(var bc in notInContacts) {
			bc.Deactivate();
		}

		// Activate things things that just got contacted. 
		var newInContacts = newContacts.Keys.Where(bc => !prevContacts.ContainsKey(bc));
		foreach(var bc in newInContacts) {
			bc.Activate(newContacts[bc]); // Activate the thing. 
		}

		var beenInContacts = newContacts.Keys.Where(bc => prevContacts.ContainsKey(bc));
		foreach(var bc in beenInContacts) {
			bc.BCUpdate(newContacts[bc]);
		}


		positions.Add(startPosition);

		if(!terminate) {
			positions.Add(startPosition + (direction.normalized * length));
		}

		_lr.positionCount = positions.Count;
		_lr.SetPositions(positions.ToArray());
	}
}
