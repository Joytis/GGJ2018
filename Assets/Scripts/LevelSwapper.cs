using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSwapper : MonoBehaviour {

    public string mainMenuName;
	public List<string> levelNames;
	List<string> currentLevels;

	void Awake() {
		currentLevels = levelNames.ToList();
	}

    public bool HasNextLevel() {
        return currentLevels.Count > 0;
    }

    public bool StartNextLevel() {
    	if(currentLevels.Count > 0) {
            StartCoroutine(LoadNextSceneAsync(currentLevels[0]));
            currentLevels.RemoveAt(0);
            return true;
    	}
    	else return false;
    }

    public void FuckIT () {
        if (HasNextLevel()) {
            StartCoroutine(LoadNextSceneAsync(currentLevels[currentLevels.Count - 1]));
            currentLevels.Clear();
        }
    }

    public void Poplevel() {
        currentLevels.RemoveAt(0);
    }

    public void LoadMainMenu() {
        StartCoroutine(LoadNextSceneAsync(mainMenuName));
    }

    public bool Contains(string level) {
    	return levelNames.Contains(level);
    }

    public void Reset() {
    	currentLevels = levelNames.ToList();
    }

    IEnumerator LoadNextSceneAsync(string lev) {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(lev);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone) { yield return null; }
    }
}