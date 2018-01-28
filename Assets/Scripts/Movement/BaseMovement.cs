using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    public float thrust;
    public float turn;
    //public Rigidbody shit;
    float _maxTurn = 5f;
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
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A)) {
            _rotation += turn;
            _rotation = (_rotation > _maxTurn ? _maxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D)) {
            _rotation -= turn;
            _rotation = (_rotation < -_maxTurn ? -_maxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
        }
        else {
            _rotation = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) { _direction = /*shit.velocity = shit.angularVelocity =*/ _rigidBody.velocity = _rigidBody.angularVelocity = transform.position = Vector3.zero; }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { _force = ForceMode.Force; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { _force = ForceMode.Acceleration; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { _force = ForceMode.Impulse; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { _force = ForceMode.VelocityChange; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
    }

    void FixedUpdate () {
        // Key input bindings
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            _direction = transform.forward;
            _rigidBody.AddForce(_direction * thrust, _force);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            _direction = -transform.forward;
            _rigidBody.AddForce(_direction * thrust, _force);
        }

        if (_rigidBody.angularVelocity.magnitude > 0.1) {
            _rigidBody.angularVelocity *= 0.9f;
        }
        else if (_rigidBody.angularVelocity.magnitude > 0) {
            _rigidBody.angularVelocity = Vector3.zero;
        }
        //shit.velocity = shit.angularVelocity = Vector3.zero;
    }
    #endregion
}
