using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{
    public static ChangeMaterialColor Instance;
    public Material[] materialList;

    // Use this for initialization
    void Awake () {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void ChangeColor (int index, Color color) {
        Instance.materialList[index].color = color;
    }
}
