using UnityEngine;

public class ForceShitTo90MyDude : MonoBehaviour {
	void LateUpdate() {
		Vector3 urgh = transform.rotation.eulerAngles;
		urgh.z = 90;
		urgh.x = 0; 
		transform.rotation = Quaternion.Euler(urgh);
	}
}