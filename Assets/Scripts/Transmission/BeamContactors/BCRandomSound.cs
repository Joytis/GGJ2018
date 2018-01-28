using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BCRandomSound : MonoBehaviour, IBeamContact {

	public List<int> soundIndeces;

	// Accept raycast hit for contact point. 
	public void Activate(RaycastHit hit) {
		if(soundIndeces.Count > 0) {
			var index = soundIndeces[Random.Range(0, soundIndeces.Count)];
		
			if (AudioController.Instance != null) { AudioController.Instance.PlayAudio(index); }
		}
	}

	public void Deactivate() {
		// foreach(var p in _particles) 
		// 	p.Stop();
	}

	public void BCUpdate(RaycastHit hit) {
		// foreach(var p in _particles) {
		// 	Reposition(hit, p);
		// }
	}

	// move the particle effects to be 
	void Reposition(RaycastHit hit, ParticleSystem p) {
		// // Reposition to be on top of point. 
		// var offset = hit.point - transform.position;
		// offset = new Vector3(
		// 	offset.x / p.transform.localScale.x,
		// 	offset.y / p.transform.localScale.y,
		// 	offset.z / p.transform.localScale.z
		// );

		// var shape = p.shape;

		// // Map the normal to the rotation  of particle system. 
		// var normal = hit.normal;
		// var extraRotation = Quaternion.Inverse(transform.rotation);
		// var q = Quaternion.LookRotation(normal, Vector3.forward - transform.rotation.eulerAngles);

		// // Shange rotation and position of shape. 
		// shape.rotation = (extraRotation * q).eulerAngles; 
		// shape.position = extraRotation * offset;
	}
}
