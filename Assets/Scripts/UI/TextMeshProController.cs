using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProController : MonoBehaviour
{
    public Color Hightlighted;
    public Color Pressed;

    TextMeshProUGUI _text;
    Color _original;

    // Use this for initialization
    void Start () {
        _text = GetComponent<TextMeshProUGUI>();
        _original = _text.color;
    }

    public void SetHighlighted () {
        _text.color = Hightlighted;
    }

    public void SetPressed () {
        _text.color = Pressed;
    }

    public void ResetEffects () {
        _text.color = _original;
    }

}
