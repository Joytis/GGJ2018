using UnityEngine;
using System.Collections.Generic;

using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Singleton GameObject, me. 
	public static GameController single = null;
	public GameObject maingui;
	public GameObject gamegui;
	public string mainMenuName;
	public string gameSceneName;
	public string loseScreenName;
	public Pause _pause;
	public CanvasGroup _cg; // fucking black-ass screen all up in my buziness dawg. Geezus. 
	public CountdownTimer _cdt;

	string currentScene;

	enum PauseState {
		Paused, 
		Unpaused
	}
	PauseState currentPauseState = PauseState.Unpaused;

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
		UpdateCameraInfo();
	}

	void UpdateCameraInfo() {
		_cameraShake.Clear();

		// Grab the camera screen shake thing
		//===============================================================
		Debug.Log("Camera: " + _cameraShake);
		var cams = Camera.allCameras;
		foreach(var cam in cams) {
			var cs = cam.GetComponent<CameraShake>();
			if(cs) _cameraShake.Add(cs);
		}	
	}

	void UpdateReceiverHooks() {
		var _recv = FindObjectOfType<Receiver>();
		if(_recv) {
			_recv.RegisterDoSomething(new Receiver.DoSomething(DoSomething));
			_recv.RegisterAntiSomething(new Receiver.DoSomething(DoAntiSomething));
		}
	}

	void DoSomething() {
		_cdt.StopTimer();
	}	
	void DoAntiSomething() {
		_cdt.StartTimer();
	}

	void Start() {
		maingui.SetActive(true);
		gamegui.SetActive(false);
	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	// Do some shitty string comparison every frame. 
	void Update() {
		if(currentScene == mainMenuName) {}
		// Check to see if game should be paused or unpaised. Woo!
		else if(currentScene == gameSceneName) {
			if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
				switch(currentPauseState) {
					case PauseState.Paused:
						Unpause();
						break;
					case PauseState.Unpaused:	
						Pause();
						break;
				}
			}
		}
		else if(currentScene == loseScreenName) {}
		else {
			Debug.LogError("Scene loaded with unrecognized name.");
		}
	}

	void Pause() { 
		if(_pause) {
			maingui.SetActive(true);
			_pause.DoPause(); 
			currentPauseState = PauseState.Paused;
		}
	}
	void Unpause() { 
		if(_pause) {
			maingui.SetActive(false);
			_pause.UnPause(); 
			currentPauseState = PauseState.Unpaused;
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

	public void LoseTheGame() {
		Debug.Log("You Lose :c");
	}



	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

		_cdt.ResetTimer();
		_cdt.StopTimer();


		if(scene.name == mainMenuName) {
			maingui.SetActive(true);
			gamegui.SetActive(false);
		}
		else if(scene.name == gameSceneName) {
			maingui.SetActive(false);
			gamegui.SetActive(true);	
			_cdt.StartTimer();
		}
		else if(scene.name == loseScreenName) {
			maingui.SetActive(false);
			gamegui.SetActive(false);
		}
		else {
			Debug.LogError("Scene loaded with unrecognized name.");
		}

		_cg.alpha = 0;
		currentScene = scene.name;
		currentPauseState = PauseState.Unpaused;
		Unpause();
		UpdateCameraInfo();
		UpdateReceiverHooks();
		
	}

}
