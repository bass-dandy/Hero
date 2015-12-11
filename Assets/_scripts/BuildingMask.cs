using UnityEngine;
using System.Collections;

public class BuildingMask : MonoBehaviour {

	private Animator anim;
	
	void Start() {
		anim = GetComponentInChildren<Animator>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			anim.SetBool("WallIsVisible", false);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player") {
			anim.SetBool("WallIsVisible", true);
		}
	}
}
