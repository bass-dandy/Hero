using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class KillableHuman : MonoBehaviour, IKillable {

	public event Action OnDeath;

	public void Kill() {
		Destroy(gameObject.GetComponent<BoxCollider2D>());
		GameObject torso = transform.Find("Torso").gameObject;
		torso.GetComponent<BoxCollider2D>().enabled = true;
		
		// Send torso flying
		Rigidbody2D rb = torso.AddComponent<Rigidbody2D>();
		rb.AddForce(new Vector2(70, 200));
		rb.AddTorque(-15.0f);
		
		// Play death particles
		gameObject.GetComponentInChildren<ParticleSystem>().Play();
		
		// Remove any remaining dialogue
		foreach(Text t in gameObject.GetComponentsInChildren<Text>()) {
			Destroy(t.gameObject);
		}
		// Trigger on death event
		if(OnDeath != null) {
			OnDeath();
		}
	}
	
	public void MoveX(float dist, float speed) {
		StartCoroutine(MoveToX(transform.position.x + dist, speed));
	}
	
	IEnumerator MoveToX(float end, float speed) {
		int direction = transform.position.x < end ? 1 : -1;
		while( (direction > 0 && transform.position.x < end - 0.1f) || 
			   (direction < 0 && transform.position.x > end + 0.1f) ) 
		{
			float x = Mathf.Lerp(transform.position.x, end, speed * Time.deltaTime);
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
			yield return null;
		}
	}
}
