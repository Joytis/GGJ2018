using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem[] rearParticles;
    public ParticleSystem[] frontParticles;
    bool _isForward;
    float _rotation = 0f;
    GameObject _reflector;
    Rigidbody _rigidBody;
    ForceMode _force = ForceMode.Force;

    public int indexForPickupSound;

    #region MonoBehavoiur
    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.mass = SpaceCraftManager.Instance.Mass;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(SpaceCraftManager.Instance.Right)) {
            _rotation += SpaceCraftManager.Instance.Turn * Time.deltaTime;
            _rotation = (_rotation > SpaceCraftManager.Instance.MaxTurn ? SpaceCraftManager.Instance.MaxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
        }
        else if (Input.GetKey(SpaceCraftManager.Instance.Left)) {
            _rotation -= SpaceCraftManager.Instance.Turn * Time.deltaTime;
            _rotation = (_rotation < -SpaceCraftManager.Instance.MaxTurn ? -SpaceCraftManager.Instance.MaxTurn : _rotation);
            transform.Rotate(Vector3.up, _rotation);
        }
        else {
            _rotation = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            DropReflector();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1)) { _force = ForceMode.Force; _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { _force = ForceMode.Acceleration; _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { _force = ForceMode.Impulse; _rigidBody.velocity = transform.position = Vector3.zero; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { _force = ForceMode.VelocityChange; _rigidBody.velocity = transform.position = Vector3.zero; }
#endif
    }

    void FixedUpdate () {
        // Key input bindings
        if (Input.GetKey(SpaceCraftManager.Instance.Forward)) {
            _rigidBody.AddForce(-transform.forward * SpaceCraftManager.Instance.Thrust * SpaceCraftManager.Instance.Burst, _force);
            PlayRearParticles();
            _isForward = true;
        }
        else if (Input.GetKey(SpaceCraftManager.Instance.Back)) {
            _rigidBody.AddForce(transform.forward * SpaceCraftManager.Instance.Thrust * SpaceCraftManager.Instance.Burst, _force);
            PlayFrontParticles();
            _isForward = false;
        }
        else {
            if (_rigidBody.velocity.magnitude > 1f) {
                _rigidBody.velocity *= ((_rigidBody.velocity.magnitude > 50f) && (_rigidBody.velocity.magnitude < 10f)) ? 0.25f : 0.975f;
                if (_isForward) { PlayFrontParticles(); } else { PlayRearParticles(); }
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
            if (AudioController.Instance != null) { AudioController.Instance.PlayAudio(indexForPickupSound); }
        }
    }
    #endregion

    #region Public Methods
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
