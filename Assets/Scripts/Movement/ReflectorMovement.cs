using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorMovement : MonoBehaviour
{
    GameObject _spaceCraft;

    #region MonoBehaviour
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (_spaceCraft != null) {
            transform.position = _spaceCraft.transform.position + (Vector3.back * 50f);
        }
    }
    #endregion

    #region Public Methods
    public void SetSpaceCraft (GameObject go) {
        _spaceCraft = go;
    }

    public void DestroySelf () {
        Destroy(this);
        _spaceCraft = null;
    }
    #endregion
}
