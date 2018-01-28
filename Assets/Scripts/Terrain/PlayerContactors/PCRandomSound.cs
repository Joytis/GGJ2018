using System.Collections.Generic;
using UnityEngine;

public class PCRandomSound : MonoBehaviour, IPlayerContact {
	public List<int> soundIndeces;

	void OnCollisionEnter(Collision collision) {
		if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
			// Debug.Log("Collision with player");
			DoShit(collision);
		}
	}

	public void DoShit(Collision collision) {

		if(soundIndeces.Count > 0) {
			var index = soundIndeces[Random.Range(0, soundIndeces.Count)];
		
			if (AudioController.Instance != null) { AudioController.Instance.PlayAudio(index); }
		}
	}
}