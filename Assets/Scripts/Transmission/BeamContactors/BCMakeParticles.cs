using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCMakeParticles : MonoBehaviour, IBeamContact {

	public List<ParticleSystem> _particles;

	// Accept raycast hit for contact point. 
	public void Activate(RaycastHit hit) {
		foreach(var p in _particles) 
			p.Play();
	}

	public void Deactivate() {
		foreach(var p in _particles) 
			p.Stop();
	}

	public void BCUpdate(RaycastHit hit) {
		Reposition(hit);
	}

	// move the particle effects to be 
	void Reposition(RaycastHit hit) {
		// Reposition to be on top of point. 
		foreach(var p in _particles) {
			var offset = hit.point - transform.position;
			offset = new Vector3(
				offset.x / transform.localScale.x,
				offset.y / transform.localScale.y,
				offset.z / transform.localScale.z
			);



			Debug.Log(hit.point);
			Debug.Log(transform.position);

			var shape = p.shape;

			// Map the normal to the rotation  of particle system. 
			var normal = hit.normal;
			Debug.DrawRay(hit.point, hit.normal * 100);
			var extraRotation = Quaternion.Inverse(transform.rotation);
			var q = Quaternion.LookRotation(normal, Vector3.forward - transform.rotation.eulerAngles);

			// Shange rotation and position of shape. 
			shape.rotation = q.eulerAngles; 
			shape.position = extraRotation * offset;
		}
	}
}
