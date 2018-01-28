using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PollBaseFuckIt : MonoBehaviour {

	public BaseManager _bm;
	public TextMeshProUGUI _tmp;
	
	// Update is called once per frame
	void Update () {	
		_bm = BaseManager.Instance;
		// _tmp = GetComponent<TextMeshProUGUI>();
		if(_bm != null) {
			_tmp.text = _bm.GetNumReflectorsLeft().ToString();
		}	
	}
}
