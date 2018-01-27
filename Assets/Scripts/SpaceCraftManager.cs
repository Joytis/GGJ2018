using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraftManager : MonoBehaviour
{
    public enum SpaceCraft
    {
        Craft1,
        Craft2,
        Craft3,
        Craft4,
        Craft5,
        Craft6
    }

    public SpaceCraft current;
    public GameObject[] SpaceCrafts;
    GameObject spaceCraft;

    #region MonoBehaviour
    void Awake () {
        UpdateCraft(current);
    }
    #endregion

    #region Public Methods
    public void UpdateCraft (SpaceCraft craft) {
        current = craft;

        if (spaceCraft != null) {
            DestroyImmediate(spaceCraft);
            spaceCraft = null;
        }

        spaceCraft = Instantiate(SpaceCrafts[(int)current], transform);
        spaceCraft.transform.position = Vector3.zero;
        spaceCraft.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    #endregion
}
