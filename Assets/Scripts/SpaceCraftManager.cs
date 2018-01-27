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
    public Transform StartingPosition;
    GameObject spaceCraft = null;

    #region MonoBehaviour
    void Awake () {
        UpdateCraft(current);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.U)) {
            current = (int)current == SpaceCrafts.Length -1 ? SpaceCraft.Craft1 : current + 1;
            UpdateCraft(current);
        }
    }
    #endregion

    #region Public Methods
    public void UpdateCraft (SpaceCraft craft) {
        current = craft;
        Vector3 pos = StartingPosition.position;
        Vector3 vel = Vector3.zero;
        Quaternion rot = StartingPosition.rotation;

        if (spaceCraft != null) {
            pos = spaceCraft.transform.position;
            rot = spaceCraft.transform.rotation;
            vel = spaceCraft.GetComponent<Rigidbody>().velocity;
            DestroyImmediate(spaceCraft);
            spaceCraft = null;
        }

        spaceCraft = Instantiate(SpaceCrafts[(int)current], pos, rot);
        spaceCraft.GetComponent<Rigidbody>().velocity = vel;
    }
    #endregion
}
