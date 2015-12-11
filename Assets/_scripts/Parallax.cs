using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	[SerializeField] private float speedScale;
	
	private Transform cameraPos;
	
	void Start() {
		cameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	void FixedUpdate () {
		transform.position = new Vector3(cameraPos.position.x / speedScale, transform.position.y, transform.position.z);
	}
}
