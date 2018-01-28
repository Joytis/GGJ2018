using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorNode : MonoBehaviour {

    //public float strength;
    //public Color color;
    bool _canPickUp = false;
    Vector3 _centerOffset = new Vector3(5f, 13.4f, 5f);

    void Update () {
        //Debug.Log(string.Format("{0}   {1}", _canPickUp, Vector3.Distance(SpaceCraftManager.Instance.GetSpaceCraft.transform.position, transform.position + _centerOffset)));
        if (_canPickUp) {
            if (Input.GetKeyDown(KeyCode.Space) && Vector3.Distance(SpaceCraftManager.Instance.GetSpaceCraft.transform.position, transform.position + _centerOffset) < 45f) {
                gameObject.AddComponent<ReflectorMovement>();
                SpaceCraftManager.Instance.GetPlayerMovement.SetReflector(gameObject);
            }
        }
    }

    public void PickUpEnabled(bool enabled) {
        _canPickUp = enabled;
        this.enabled = enabled;
    }
}
