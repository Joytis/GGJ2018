using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float shit1 = 0.9f;
    public float shit2 = 0.99f;
    public float thrust;
    public float turn;
    public bool invert;
    public ParticleSystem[] rearParticles;
    public ParticleSystem[] frontParticles;
    bool _isForward;
    float _maxTurn = 5f;
    float _rotation = 0f;
    GameObject _reflector;
    Rigidbody _rigidBody;
    Vector3 _direction = Vector3.zero;
    ForceMode _force = ForceMode.Force;

    KeyCode _forward = KeyCode.UpArrow;
    KeyCode _back = KeyCode.DownArrow;
    KeyCode _left = KeyCode.LeftArrow;
    KeyCode _right = KeyCode.RightArrow;

    #region MonoBehavoiur
    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(_right)) {
            _rotation += turn;
            _rotation = (_rotation > _maxTurn ? _maxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
            //_canBigThrust = true;
        }
        else if (Input.GetKey(_left)) {
            _rotation -= turn;
            _rotation = (_rotation < -_maxTurn ? -_maxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
            //_canBigThrust = true;
        }
        else {
            _rotation = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            DropReflector();
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

        if (Input.GetKeyDown(KeyCode.Alpha1)) { _force = ForceMode.Force; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { _force = ForceMode.Acceleration; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { _force = ForceMode.Impulse; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { _force = ForceMode.VelocityChange; _direction = _rigidBody.velocity = transform.position = Vector3.zero; }
    }

    void FixedUpdate () {
        // Key input bindings
        //if (Input.GetKeyDown(_forward) && !Input.GetKey(_back) && _canBigThrust) {
        //    _direction = invert ? transform.forward : -transform.forward;
        //    _rigidBody.AddForce(_direction * thrust * 2f, ForceMode.Impulse);
        //    _canBigThrust = false;
        //    StopAllCoroutines();
        //    StartCoroutine(CoolDownForward());
        //    if (invert) { PlayFrontParticles(); } else { PlayRearParticles(); }
        //}
        //else if (Input.GetKeyDown(_back) && !Input.GetKey(_forward) && _canBigThrust) {
        //    _direction = invert ? -transform.forward : transform.forward;
        //    _rigidBody.AddForce(_direction * thrust * 2f, ForceMode.Impulse);
        //    StopAllCoroutines();
        //    StartCoroutine(CoolDownReverse());
        //    if (invert) { PlayRearParticles(); } else { PlayFrontParticles(); }
        //}

        if (Input.GetKey(_forward)) {
            _direction = invert ? transform.forward : -transform.forward;
            _rigidBody.AddForce(_direction * thrust /* ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 3f : 1f)*/, _force);
            if (invert) { PlayFrontParticles(); } else { PlayRearParticles(); }
            _isForward = true;
        }
        else if (Input.GetKey(_back)) {
            _direction = invert ? -transform.forward : transform.forward;
            _rigidBody.AddForce(_direction * thrust /* ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 3f : 1f)*/, _force);
            if (invert) { PlayRearParticles(); } else { PlayFrontParticles(); }
            _isForward = false;
        }
        else {
            if (_rigidBody.velocity.magnitude > 1f) {
                Debug.Log(_rigidBody.velocity.magnitude);
                _rigidBody.velocity *= ((_rigidBody.velocity.magnitude > 50f) && (_rigidBody.velocity.magnitude < 10f)) ? 0.25f : 0.975f;
                if (_isForward && !invert) { PlayFrontParticles(); } else { PlayRearParticles(); }
            }
            else if (_rigidBody.velocity.magnitude > 0f) {
                _rigidBody.velocity = Vector3.zero;
            }
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
        _reflector.GetComponent<ReflectorMovement>().SetSpaceCraft(gameObject);
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
    //IEnumerator CoolDownForward () {
    //    yield return new WaitForSeconds(2f);
    //    _canBigThrust = true;
    //}

    //IEnumerator CoolDownReverse () {
    //    yield return new WaitForSeconds(2f);
    //    _canBigThrust = true;
    //}

    void PlayRearParticles () {
        foreach (ParticleSystem p in rearParticles) {
            p.Emit(1);
        }
    }

    void PlayFrontParticles () {
        foreach (ParticleSystem p in frontParticles) {
            p.Emit(1);
        }
    }
    #endregion
}
