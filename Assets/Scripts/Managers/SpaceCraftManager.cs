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
    public static SpaceCraftManager Instance;
    public SpaceCraft current;
    public GameObject[] SpaceCrafts;
    public Transform StartingPosition;
    GameObject _spaceCraft = null;
    PlayerMovement _playerMovement = null;

    #region MonoBehaviour
    void Awake () {
        if (Instance == null) {
            Instance = this;
        }

        Instance.UpdateCraft(current);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.U)) {
            current = (int)current == SpaceCrafts.Length - 1 ? SpaceCraft.Craft1 : current + 1;
            Instance.UpdateCraft(current);
        }
    }
    #endregion

    #region Public Methods
    public void UpdateCraft (SpaceCraft craft) {
        current = craft;
        Vector3 pos = StartingPosition.position;
        Vector3 vel = Vector3.zero;
        Quaternion rot = StartingPosition.rotation;
        GameObject reflector = null;

        if (_spaceCraft != null) {
            pos = _spaceCraft.transform.position;
            rot = _spaceCraft.transform.rotation;
            vel = _spaceCraft.GetComponent<Rigidbody>().velocity;
            reflector = _spaceCraft.GetComponent<PlayerMovement>().GetReflector();
            DestroyImmediate(_spaceCraft);
            _spaceCraft = null;
            _playerMovement = null;
        }

        _spaceCraft = Instantiate(SpaceCrafts[(int)current], pos, rot);
        _spaceCraft.GetComponent<Rigidbody>().velocity = vel;
        _playerMovement = _spaceCraft.GetComponent<PlayerMovement>();
        _playerMovement.SetReflector(reflector);
    }

    public GameObject GetSpaceCraft { get { return Instance._spaceCraft; } }
    public PlayerMovement GetPlayerMovement { get { return Instance._playerMovement; } }
    #endregion
}
