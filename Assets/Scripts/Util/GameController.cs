using UnityEngine;

public class GameController : MonoBehaviour {

	// Singleton GameObject, me. 
	public static GameController single = null;


	CameraShake _cameraShake;

	// Should be the SINGLE player instance. When a player Awakes, they will register themself
	//		as the active player. Various systems will then snag the player 
	// NOTE(clark): You should NEVER GET THE PLAYER FROM AN ACTIVE SCENE on AWAKE! 
	// NOTE(clark): ONLY CALL THIS PROPERTY ON START!!!
	public GameObject Player { get; private set; }

	// Should be the SINGLE LEVELINFO instance.  
	// NOTE(clark): You should NEVER GET THE PLAYER FROM AN ACTIVE SCENE on AWAKE! 
	public GameObject LevelInfo { get; private set; }

	void Awake() {
		if(single == null) {
			single = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
			return;
		}

		// Grab the player and LeveInfo in the scene!
		//==============================================================
		Player = GameObject.FindWithTag("Player");
		LevelInfo = GameObject.FindWithTag("LevelInfo");

		// Grab the camera screen shake thing
		//===============================================================
		_cameraShake = Camera.main.GetComponent<CameraShake>();

	}

	public void Respawn() {
		if(Player) {
			Player.transform.SetPositionAndRotation(new Vector3(0, 7, 0), Quaternion.identity);
		}
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
