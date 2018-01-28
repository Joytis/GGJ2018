using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemSingle : MonoBehaviour {


    public static EventSystemSingle Instance;

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
