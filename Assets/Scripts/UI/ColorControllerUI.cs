using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorControllerUI : MonoBehaviour
{
    public Transform content;
    public GameObject item;
    public ColorPicker picker;
    public GameObject pickerBlocker;
    List<Image> _colorList;
    Image _currentImg;

    // Use this for initialization
    void Start () {
        Init();
    }

    public void Init () {
        _colorList = new List<Image>();
        foreach (Material mat in ChangeMaterialColor.Instance.materialList) {
            GameObject go = Instantiate(item, content);
            _colorList.Add(go.GetComponentInChildren<Image>());
            Button b = go.GetComponentInChildren<Button>();
            Image i = _colorList[_colorList.Count - 1];
            i.color = mat.color;
            b.onClick.AddListener(() => SetPicker(i));

            go.GetComponentInChildren<TextMeshProUGUI>().text = mat.name;
        }
    }

    public void SetPicker (Image i) {
        pickerBlocker.SetActive(true);
        picker.gameObject.SetActive(true);
        picker.onValueChanged.AddListener(UpdateColor);
        _currentImg = i;
    }

    public void UpdateColor (Color color) {
        _currentImg.color = color;
    }

    public void ClearPicker () {
        pickerBlocker.SetActive(false);
        picker.gameObject.SetActive(false);
        picker.onValueChanged.RemoveListener(UpdateColor);
        _currentImg = null;
    }

    public void OnSave () {
        for (int i = 0; i < _colorList.Count; ++i) {
            ChangeMaterialColor.Instance.ChangeColor(i, _colorList[i].color);
        }
    }
}
