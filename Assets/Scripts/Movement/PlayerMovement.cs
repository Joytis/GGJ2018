using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust;
    public float turn;
    public bool invert;
    public float _maxTurn = 5f;
    float _rotation = 0f;
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
        if (Input.GetKey(KeyCode.RightArrow)) {
            _rotation += turn;
            _rotation = (_rotation > _maxTurn ? _maxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            _rotation -= turn;
            _rotation = (_rotation < -_maxTurn ? -_maxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
        }
        else {
            _rotation = 0f;
        }

        if (Input.GetKeyDown(KeyCode.F)) { _force = ForceMode.Force; _direction = _rigidBody.velocity = _rigidBody.angularVelocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.A)) { _force = ForceMode.Acceleration; _direction = _rigidBody.velocity = _rigidBody.angularVelocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.I)) { _force = ForceMode.Impulse; _direction = _rigidBody.velocity = _rigidBody.angularVelocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.V)) { _force = ForceMode.VelocityChange; _direction = _rigidBody.velocity = _rigidBody.angularVelocity = transform.position = Vector3.zero; }
    }

    void FixedUpdate () {
        // Key input bindings
        if (Input.GetKey(KeyCode.UpArrow)) {
            _direction = invert ? -transform.forward : transform.forward;
            _rigidBody.AddForce(_direction * thrust, _force);
        }
        else if (Input.GetKey(KeyCode.DownArrow)) {
            _direction = invert ? transform.forward : -transform.forward;
            _rigidBody.AddForce(_direction * thrust, _force);
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
    #endregion
}
