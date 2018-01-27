using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public static BaseManager Instance;
    public int NumberOfReflectors;
    public GameObject ReflectorPrefab;

    #region MonoBehaviour
    // Use this for initialization
    void Awake () {
        if (Instance == null) {
            Instance = this;
        }
    }
    #endregion

    #region Public Methods
    public void SpawnReflector(PlayerMovement player) {
        if (NumberOfReflectors > 0) {
            GameObject go = Instantiate(ReflectorPrefab);
            player.SetReflector(go);
            --NumberOfReflectors;
        }
    }
    #endregion
}
