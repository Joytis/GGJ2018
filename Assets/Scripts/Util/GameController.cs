using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelSwapper))]
public class GameController : MonoBehaviour {

	// Singleton GameObject, me. 
	public static GameController single = null;
	public GameObject maingui;
	public GameObject gamegui;
	public string mainMenuName;
	public string blankSceneName;
	// public string gameSceneName;
	public string loseScreenName;
	public Pause _pause;
	public CanvasGroup _cg; // fucking black-ass screen all up in my buziness dawg. Geezus. 
	public CountdownTimer _cdt;
	LevelSwapper _levelSwapper;
	public GameObject gameTopPanel;
	public GameObject gameResPanel;
	public GameObject gameWinText;
	public GameObject gameLoseText;
	public GameObject mainMenuPanel;
	public int levelWinAudioIndex;


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
		_levelSwapper = GetComponent<LevelSwapper>();
	}

	void UpdateCameraInfo() {
		_cameraShake.Clear();

		// Grab the camera screen shake thing
		//===============================================================
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
		else if(_levelSwapper.Contains(SceneManager.GetActiveScene().name)) {
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
		// else {
		// 	Debug.LogError("Scene loaded with unrecognized name.");
		// }

		#if UNITY_EDITOR
			if(Input.GetKeyDown(KeyCode.Q)) {
				FinishLevel();
			}
		#endif
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
		ShowResults(false);
	}


	public void FinishLevel() {
		if (AudioController.Instance != null) { AudioController.Instance.PlayAudio(levelWinAudioIndex); }

		if(_levelSwapper.HasNextLevel()) {
			// Do next level stuff
			_levelSwapper.StartNextLevel();
		}
		else {
			LoadWinGame();
		}
		
	}

	public void QuitInASecond() {
		Invoke("Quit", 0.5f);
	}

	void Quit() {
		//If we are running in a standalone build of the game
		#if UNITY_STANDALONE
			//Quit the application
			Application.Quit();
		#endif

			//If we are running in the editor
		#if UNITY_EDITOR
			//Stop playing the scene
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}


	void LoadWinGame() {
		// WIN THE GAME HERE.
		//==================================================

		ShowResults(true);
		
	}

	// Two state. True for win, false for lose. 
	public void ShowResults(bool winLose) {
		gamegui.SetActive(true);
		gameTopPanel.SetActive(false);
		gameResPanel.SetActive(true);
		gameWinText.SetActive(winLose);
		gameLoseText.SetActive(!winLose);
	}

	public void HideResults() {
		gameTopPanel.SetActive(true);
		gameResPanel.SetActive(false);
	}

	public void DoNextLevelFuckIt() {
		if(_levelSwapper.HasNextLevel()) {
			_levelSwapper.StartNextLevel();
		}
	}

	public void Reset____Everything______() {
		// HideResults();
		// maingui.SetActive(true);
		// gamegui.SetActive(false);
		_levelSwapper.Reset();
		_levelSwapper.LoadMainMenu();
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

		_cdt.ResetTimer();
		_cdt.StopTimer();



		_cg.alpha = 0;
		currentScene = scene.name;
		currentPauseState = PauseState.Unpaused;
		Unpause();
		UpdateCameraInfo();
		UpdateReceiverHooks();


		if(scene.name == mainMenuName) {
			maingui.SetActive(true);
			gamegui.SetActive(false);
		}
		else if(_levelSwapper.Contains(scene.name)) {
			maingui.SetActive(false);
			gamegui.SetActive(true);	
			_cdt.StartTimer();
		}
		else if(scene.name == blankSceneName) {
			HideResults();
			gamegui.SetActive(false);	
			mainMenuPanel.SetActive(true);
			maingui.SetActive(true);
		}
		else if(scene.name == loseScreenName) {
			maingui.SetActive(false);
			gamegui.SetActive(false);
		}
		else {
			Debug.LogError("Scene loaded with unrecognized name.");
		}
	}

}
