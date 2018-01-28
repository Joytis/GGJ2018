using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public static BaseManager Instance;
    public int NumberOfReflectors;
    public GameObject ReflectorPrefab;
    int _remainingReflectors;

    #region MonoBehaviour
    // Use this for initialization
    void Awake () {
        if (Instance == null) {
            Instance = this;
        }

        Instance._remainingReflectors = Instance.NumberOfReflectors;

        // Find the POllBaseFuckIt instance and set the base manager to this. 
        var pbfi = FindObjectOfType<PollBaseFuckIt>();
        if(pbfi) {
            pbfi._bm = this;
        }
    }
    #endregion

    #region Public Methods
    public void SpawnReflector (PlayerMovement player) {
        if (_remainingReflectors > 0) {
            GameObject go = Instantiate(ReflectorPrefab);
            go.AddComponent<ReflectorMovement>();
            player.SetReflector(go);
            --_remainingReflectors;
        }
    }

    public void SetNumReflectors (int num) {
        NumberOfReflectors = num;
        _remainingReflectors = num;
    }

    public void UpdateNumReflectors (int num) {
        int used = NumberOfReflectors - _remainingReflectors;
        NumberOfReflectors = num;
        _remainingReflectors = num - used;
    }

    public int GetNumReflectorsLeft () {
        return Instance._remainingReflectors;
    }
    #endregion
}
