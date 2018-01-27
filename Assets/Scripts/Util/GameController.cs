using UnityEngine;

public class GameController : MonoBehaviour {

	// Singleton GameObject, me. 
	public static GameController single = null;

	CameraShake _cameraShake;

	void Awake() {
		if(single == null) {
			single = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
			return;
		}

		// Grab the camera screen shake thing
		//===============================================================
		_cameraShake = Camera.main.GetComponent<CameraShake>();

	}

	public void CancelCameraShake() {
		if(_cameraShake) {
			_cameraShake.RemoveAllTrauma();
		}
	}

	public void ApplyCameraShake(float amount) {
		// Just pass through to whatever component we're using. 
		if(_cameraShake) {
			_cameraShake.AddTrauma(amount);
		}
	}
}
