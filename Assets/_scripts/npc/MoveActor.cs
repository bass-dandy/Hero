using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveActor : MonoBehaviour {

	[SerializeField] private Transform end;
	[SerializeField] private float lerpStep;
	[SerializeField] private bool isPatrol;

	private Vector3 start;
	private int direction = 1;
	
	void Start() {
		start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		if(transform.position.x > end.position.x)
			direction = -1;
		StartCoroutine(FollowPath());
	}
	
	IEnumerator FollowPath() {
		while(transform.position.x != end.position.x) {
			float x = Mathf.Lerp(transform.position.x, end.position.x, lerpStep * direction * Time.deltaTime);
			if(Mathf.Abs(end.position.x - x) < 0.01f) {
				x = transform.position.x;
			}
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
			yield return null;
		}
		gameObject.GetComponentInChildren<Canvas>().enabled = true;
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, end.position);
	}
}
