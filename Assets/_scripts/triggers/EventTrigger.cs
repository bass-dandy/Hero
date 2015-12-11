using UnityEngine;
using System.Collections;
using System;

public class EventTrigger : MonoBehaviour {

	[SerializeField] private bool destroyOnEnter;
	
	public event Action OnEnter;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player") {
			OnEnter();
			if(destroyOnEnter) {
				Destroy(gameObject);
			}
		}
	}
}
