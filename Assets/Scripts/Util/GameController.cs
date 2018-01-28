using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	// Singleton GameObject, me. 
	public static GameController single = null;

	// SHAKE ALL THE FUCKING CAMERAS. 
	List<CameraShake> _cameraShake = new List<CameraShake>();

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
		Debug.Log("Camera: " + _cameraShake);
		var cams = Camera.allCameras;
		foreach(var cam in cams) {
			var cs = cam.GetComponent<CameraShake>();
			if(cs) _cameraShake.Add(cs);
		}
	}

	public void CancelCameraShake() {	
		foreach(var cam in _cameraShake)
			cam.RemoveAllTrauma();
	}

	public void ApplyCameraShake(float amount) {
		// Just pass through to whatever component we're using. 
		foreach(var cam in _cameraShake)
			cam.AddTrauma(amount);
	}

}
