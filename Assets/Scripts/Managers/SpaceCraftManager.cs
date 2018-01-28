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
    public Transform startingPosition;
    [SerializeField] [Range(1f, 4f)]
    float _burstMultiplier = 1f;
    public SpaceCraft current;
    public GameObject[] spaceCrafts;
    GameObject _spaceCraft = null;
    PlayerMovement _playerMovement = null;

    KeyCode _forward = KeyCode.UpArrow;
    KeyCode _back = KeyCode.DownArrow;
    KeyCode _left = KeyCode.LeftArrow;
    KeyCode _right = KeyCode.RightArrow;

    #region MonoBehaviour
    void Awake () {
        if (Instance == null) {
            Instance = this;
        }

        if (Instance.startingPosition == null) {
            Instance.startingPosition = GameObject.FindGameObjectWithTag("Respawn").transform;
        }

        Instance.UpdateCraft(current);
    }

#if UNITY_EDITOR
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.U)) {
            current = (int)current == spaceCrafts.Length - 1 ? SpaceCraft.Craft1 : current + 1;
            Instance.UpdateCraft(current);
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (_forward == KeyCode.UpArrow) {
                _forward = KeyCode.W;
                _back = KeyCode.S;
                _left = KeyCode.A;
                _right = KeyCode.D;
            }
            else {
                _forward = KeyCode.UpArrow;
                _back = KeyCode.DownArrow;
                _left = KeyCode.LeftArrow;
                _right = KeyCode.RightArrow;
            }
        }
    }
#endif
    #endregion

    #region Public Methods
    public void UpdateCraft (SpaceCraft craft) {
        current = craft;
        Vector3 pos = startingPosition.position;
        Vector3 vel = Vector3.zero;
        Quaternion rot = startingPosition.rotation;
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

        _spaceCraft = Instantiate(spaceCrafts[(int)current], pos, rot);
        _spaceCraft.GetComponent<Rigidbody>().velocity = vel;
        _playerMovement = _spaceCraft.GetComponent<PlayerMovement>();
        _playerMovement.SetReflector(reflector);
    }

    public GameObject GetSpaceCraft { get { return Instance._spaceCraft; } }
    public PlayerMovement GetPlayerMovement { get { return Instance._playerMovement; } }
    public float Burst { get { return ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) ? 1f : Instance._burstMultiplier); } }

    public KeyCode Forward { get { return _forward; } }
    public KeyCode Back { get { return _back; } }
    public KeyCode Left { get { return _left; } }
    public KeyCode Right { get { return _right; } }
    #endregion
}
