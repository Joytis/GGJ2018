using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorMovement : MonoBehaviour
{
    GameObject _spaceCraft;
    ReflectorNode _node;

    #region MonoBehaviour
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (_spaceCraft != null) {
            transform.position = _spaceCraft.transform.position + (_spaceCraft.transform.forward * 50f) + (_spaceCraft.transform.up * -13f);
            transform.rotation = _spaceCraft.transform.rotation * Quaternion.Euler(0f,0f,90f);
        }
    }
    #endregion

    #region Public Methods
    public void SetSpaceCraft (GameObject go) {
        _spaceCraft = go;
        transform.position = _spaceCraft.transform.position + (_spaceCraft.transform.forward * 50f) + (_spaceCraft.transform.up * -13f);
        _node = GetComponent<ReflectorNode>();
        _node.PickUpEnabled(false);
    }

    public void DestroySelf () {
        _node.PickUpEnabled(true);
        Destroy(this);
        _spaceCraft = null;
    }
    #endregion
}
