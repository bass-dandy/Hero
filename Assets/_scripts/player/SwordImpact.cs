using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordImpact : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		IKillable k = InterfaceUtils.GetInterface<IKillable>(other.gameObject);
		if(k != null) {
			k.Kill();
		}
	}
}
