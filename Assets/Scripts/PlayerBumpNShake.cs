using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerBumpNShake : MonoBehaviour {

	Rigidbody _rb;
	GameController _gc;


	void Awake() {
		_rb = GetComponent<Rigidbody>();
		var obj = GameObject.FindWithTag("GameController");
		_gc = obj.GetComponent<GameController>();
	}

	void OnCollisionEnter(Collision coll) {
		_gc.ApplyCameraShake(0.1f);
	}
}