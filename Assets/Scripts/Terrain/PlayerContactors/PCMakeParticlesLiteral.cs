
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCMakeParticlesLiteral : MonoBehaviour, IPlayerContact {

	public List<ParticleSystem> _particles;

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
		Debug.Log("WE DID IT");
		foreach(var p in _particles) {
			Reposition(collision, p);
			p.Play();
		}
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

		var shape = p.shape;

		// Map the normal to the rotation  of particle system. 
		var q = Quaternion.LookRotation(normal, Vector3.forward);

		// Shange rotation and position of shape. 
		shape.rotation = q.eulerAngles; 
		p.transform.position = point;
	}
}