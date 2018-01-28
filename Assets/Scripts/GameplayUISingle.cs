using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUISingle : MonoBehaviour {


    public static GameplayUISingle Instance;

    void Awake () {
        if (Instance == null) {
            Instance = this;
        }

        else {
            Destroy(gameObject);
            return;
        }
    }
}
