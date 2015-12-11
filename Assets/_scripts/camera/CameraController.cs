using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField] private float lerpStep;
	
	private Transform playerPos;
	private float xMax = float.MinValue;
	private float xMin = float.MaxValue;
	private bool isDetached = false;
	
	void Start() {
		playerPos = GameObject.FindGameObjectWithTag("Player").transform;
		float extent = Camera.main.orthographicSize * Screen.width / Screen.height;
		
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("CameraBound")) {
			if(g.transform.position.x > xMax)
				xMax = g.transform.position.x;
			if(g.transform.position.x < xMin)
				xMin = g.transform.position.x;
		}
		xMax -= extent;
		xMin += extent;
	}
	
	void FixedUpdate () {
		if(!isDetached) {
			float x = Mathf.Clamp(playerPos.position.x, xMin, xMax);
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
		}
	}
	
	void AffixRight() {
		isDetached = true;
		StartCoroutine(LerpToPoint(xMax));
	}
	
	IEnumerator LerpToPoint(float dest) {
		while(transform.position.x < dest) {
			float x = Mathf.Lerp(transform.position.x, dest, lerpStep * Time.deltaTime);
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
			yield return null;
		}
	}
}
