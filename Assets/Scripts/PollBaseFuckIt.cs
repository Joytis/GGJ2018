using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PollBaseFuckIt : MonoBehaviour {

	BaseManager _bm;
	TextMeshProUGUI _tmp;

	void OnSceneLoaded() {
		_bm = FindObjectOfType<BaseManager>();
		_tmp = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {	
		if(_bm) {
			_tmp.text = _bm.GetNumReflectorsLeft().ToString();
		}	
	}
}
