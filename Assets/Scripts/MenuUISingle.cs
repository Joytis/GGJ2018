using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUISingle : MonoBehaviour {


    public static MenuUISingle Instance;

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
