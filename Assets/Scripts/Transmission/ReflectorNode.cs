using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorNode : MonoBehaviour
{
    bool _canPickUp = false;
    Vector3 _centerOffset = new Vector3(5f, 13.4f, 5f);
    public int indexForPickupSound;

    void Update () {
        if (_canPickUp) {
            if (Input.GetKeyDown(KeyCode.Space) && Vector3.Distance(SpaceCraftManager.Instance.GetSpaceCraft.transform.position, transform.position + _centerOffset) < 45f) {
                gameObject.AddComponent<ReflectorMovement>();
                SpaceCraftManager.Instance.GetPlayerMovement.SetReflector(gameObject);
                if (AudioController.Instance != null) { AudioController.Instance.PlayAudio(indexForPickupSound); }
            }
        }
    }

    public void PickUpEnabled (bool enabled) {
        _canPickUp = enabled;
        this.enabled = enabled;
    }
}
