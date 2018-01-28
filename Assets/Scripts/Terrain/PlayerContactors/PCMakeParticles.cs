
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PCMakeParticles : MonoBehaviour, IPlayerContact {

	public List<ParticleSystem> _particles;
	public UnityEvent Sound;

	void Awake() {
		// Force the particle systems to NOT loop!
		foreach(var p in _particles) {
			var main = p.main;
			main.loop = false;
		}
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
			Debug.Log("Collision with player");
			DoShit(collision);
		}
	}

	public void DoShit(Collision collision) {
		foreach(var p in _particles) {
			Reposition(collision, p);
			p.Play();
		}

		if (Sound != null) { Sound.Invoke(); }
	}

	// move the particle effects to be 
	void Reposition(Collision hit, ParticleSystem p) {

		// Average the contact points and normals
		var contacts = hit.contacts;
		var norm = Vector3.zero;
		var pt = Vector3.zero;
		foreach(var c in contacts) {
			norm += c.normal;
			pt += c.point;
		}
		var point = pt / contacts.Length;
		var normal = pt / contacts.Length;

		// Reposition to be on top of point. 
		var offset = point - transform.position;
		offset = new Vector3(
			offset.x / p.transform.localScale.x,
			offset.y / p.transform.localScale.y,
			offset.z / p.transform.localScale.z
		);

		var shape = p.shape;

		// Map the normal to the rotation  of particle system. 
		var extraRotation = Quaternion.Inverse(transform.rotation);
		var q = Quaternion.LookRotation(normal, Vector3.forward - transform.rotation.eulerAngles);

		// Shange rotation and position of shape. 
		shape.rotation = (extraRotation * q).eulerAngles; 
		shape.position = extraRotation * offset;
	}
}