using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO(clark): Have reflector nodes that have reflcetion strength. 

[RequireComponent(typeof(LineRenderer))]
public class Transmitter : MonoBehaviour {

	public float length = 50;
	public LayerMask collisions;
	public int maxBounceCount = 20;

	int currentCount = 0;

	List<Vector3> positions = new List<Vector3>();

	LineRenderer _lr;

	void Awake()  {
		_lr = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update () {

		positions.Clear();

		Vector3 startPosition = transform.position;
		Vector3 direction = transform.forward;
		
		RaycastHit hit;
		currentCount = 0;

		bool terminate = false;

		// Continue shooting out rays until things don't get hit. 
		while(currentCount < maxBounceCount &&
			  Physics.Raycast(startPosition, direction, out hit, length, collisions) ) {

			// barycentricCoordinate	The barycentric coordinate of the triangle we hit.
			// collider	The Collider that was hit.
			// distance	The distance from the ray's origin to the impact point.
			// lightmapCoord	The uv lightmap coordinate at the impact point.
			// normal	The normal of the surface the ray hit.
			// point	The impact point in world space where the ray hit the collider.
			// rigidbody	The Rigidbody of the collider that was hit. If the collider is not attached to a rigidbody then it is null.
			// textureCoord	The uv texture coordinate at the collision location.
			// textureCoord2	The secondary uv texture coordinate at the impact point.
			// transform	The Transform of the rigidbody or collider that was hit.
			// triangleIndex	The index of the triangle that was hit.

			positions.Add(startPosition);
			startPosition = hit.point;
			direction = Vector3.Reflect(direction, hit.normal);

			currentCount++;

			// Check for special collisions()
			var go = hit.collider.gameObject;

			if(go.layer == LayerMask.NameToLayer("Receiver")) {
				var rec = go.GetComponent<Receiver>();
				if(rec == null) {
					rec = go.GetComponentInChildren<Receiver>();
				}
				rec.DoThing();
				terminate = true;
			}
		}

		positions.Add(startPosition);

		if(!terminate) {
			positions.Add(startPosition + (direction.normalized * length));
		}

		_lr.positionCount = positions.Count;
		_lr.SetPositions(positions.ToArray());
	}
}
