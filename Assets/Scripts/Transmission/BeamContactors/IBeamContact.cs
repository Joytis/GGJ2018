using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeamContact {
	void Activate(RaycastHit hit);
	void Deactivate();
	void BCUpdate(RaycastHit hit);
}
