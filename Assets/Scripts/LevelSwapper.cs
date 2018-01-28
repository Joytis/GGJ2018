using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSwapper : MonoBehaviour {

	public List<string> levelNames;
	List<string> currentLevels;

	void Awake() {
		currentLevels = levelNames.ToList();
	}

    public bool StartNextLevel() {
    	if(currentLevels.Count > 0) {
            StartCoroutine(LoadNextSceneAsync());
            return true;
    	}
    	else return false;
    }

    public void Poplevel() {
        currentLevels.RemoveAt(0);
    }

    public bool Contains(string level) {
    	return levelNames.Contains(level);
    }

    public void Reset() {
    	currentLevels = levelNames.ToList();
    }

    IEnumerator LoadNextSceneAsync() {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentLevels[0]);
        currentLevels.RemoveAt(0);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone) { yield return null; }
    }
}