using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour {

	private GameObject cam;

	void Start() {
		cam = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			cam.SendMessage("AffixRight");
			Destroy(gameObject);
		}
	}
}
