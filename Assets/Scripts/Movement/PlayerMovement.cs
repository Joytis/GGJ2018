using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust;
    public float turn;
    public bool invert;
    bool _canBigThrust;
    float _maxTurn = 5f;
    float _rotation = 0f;
    GameObject _reflector;
    Rigidbody _rigidBody;
    Vector3 _direction = Vector3.zero;
    ForceMode _force = ForceMode.Force;

    #region MonoBehavoiur
    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A)) {
            _rotation += turn;
            _rotation = (_rotation > _maxTurn ? _maxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
            _canBigThrust = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D)) {
            _rotation -= turn;
            _rotation = (_rotation < -_maxTurn ? -_maxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
            _canBigThrust = true;
        }
        else {
            _rotation = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            DropReflector();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { _force = ForceMode.Force; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { _force = ForceMode.Acceleration; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { _force = ForceMode.Impulse; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { _force = ForceMode.VelocityChange; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
    }

    void FixedUpdate () {
        // Key input bindings
        if (((Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.W) && !Input.GetKey(KeyCode.S))) && _canBigThrust) {
            _direction = invert ? transform.forward : -transform.forward;
            _rigidBody.AddForce(_direction * thrust * 2f, ForceMode.Impulse);
            _canBigThrust = false;
            StopAllCoroutines();
            StartCoroutine(CoolDownForward());
        }
        else if (((Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.S) && !Input.GetKey(KeyCode.W))) && _canBigThrust) {
            _direction = invert ? -transform.forward : transform.forward;
            _rigidBody.AddForce(_direction * thrust * 2f, ForceMode.Impulse);
            StopAllCoroutines();
            StartCoroutine(CoolDownReverse());
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            _direction = invert ? transform.forward : -transform.forward;
            _rigidBody.AddForce(_direction * thrust, _force);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            _direction = invert ? -transform.forward : transform.forward;
            _rigidBody.AddForce(_direction * thrust, _force);
        }

        if (_rigidBody.angularVelocity.magnitude > 0.1) {
            _rigidBody.angularVelocity *= 0.9f;
        }
        else if (_rigidBody.angularVelocity.magnitude > 0) {
            _rigidBody.angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag("Base") && _reflector == null) {
            BaseManager.Instance.SpawnReflector(this);
        }
    }
    #endregion

    #region Public Methods
    public void SetThrust (float thrust) {
        this.thrust = thrust;
    }

    public void SetTurn (float turn) {
        this.turn = turn;
    }

    public void SetMaxTurn (float maxTurn) {
        _maxTurn = maxTurn;
    }

    public void Invert () {
        invert = !invert;
    }

    public void SetMass (float mass) {
        _rigidBody.mass = mass;
    }

    public void SetDrag (float drag) {
        _rigidBody.drag = drag;
    }

    public void SetAngularDrag (float angularDrag) {
        _rigidBody.angularDrag = angularDrag;
    }

    public void SetReflector (GameObject go) {
        _reflector = go;
        if (_reflector == null) { return; }
        _reflector.GetComponent<ReflectorMovement>().SetSpaceCraft(this.gameObject);
    }

    public GameObject GetReflector () {
        return _reflector;
    }

    public void DropReflector () {
        if (_reflector == null) { return; }
        _reflector.GetComponent<ReflectorMovement>().DestroySelf();
        _reflector = null;
    }
    #endregion

    #region Private Methods
    IEnumerator CoolDownForward () {
        yield return new WaitForSeconds(2f);
        _canBigThrust = true;
    }

    IEnumerator CoolDownReverse () {
        yield return new WaitForSeconds(2f);
        _canBigThrust = true;
    }
    #endregion
}
