using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PCBounceABit : MonoBehaviour, IPlayerContact {

	public float appliedForce;
	Rigidbody _rb;

	void Awake() {
		_rb = GetComponent<Rigidbody>();
	}	

	void OnCollisionEnter(Collision collision) {
		if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Player") || 
		   collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstructables")  
			) {
			// Debug.Log("Collision with player");
			DoShit(collision);
		}
	}

	public void DoShit(Collision hit) {
		// Average the contact points and normals
		var contacts = hit.contacts;
		var norm = Vector3.zero;
		var pt = Vector3.zero;
		foreach(var c in contacts) {
			norm += c.normal;
			pt += c.point;
		}
		var normal = pt / contacts.Length;

		var direction = normal * -1;

		var force = direction * appliedForce;
		force = new Vector3(force.x, 0f, force.z);

		_rb.AddForce(force);
	}
}