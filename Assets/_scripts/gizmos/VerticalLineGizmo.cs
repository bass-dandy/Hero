using UnityEngine;
using System.Collections;

public class VerticalLineGizmo : MonoBehaviour {

	[SerializeField] private Color color;
	
	private Vector3 length = new Vector3(0, 100, 0);

	void OnDrawGizmos() {
		Gizmos.color = color;
		Vector3 center = transform.position;
		Gizmos.DrawLine(center - length, center + length);
	}
}
