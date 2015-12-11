using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float maxSpeed;
	[SerializeField] private float accel;
	[SerializeField] private float decel;
	[SerializeField] private float jumpForce;
	
	private Rigidbody2D rb;
	private bool flip = false;
	private float scaleX;
	private bool isFrozen = false;
	
	void Start() {
		rb = gameObject.GetComponent<Rigidbody2D>();
		scaleX = transform.localScale.x;
	}
	
	void Update () {
		// Move horizontally
		if(Input.GetKey(KeyCode.LeftArrow) && !isFrozen) {
			MoveH(-accel);
			flip = true;
		} else if(Input.GetKey(KeyCode.RightArrow) && !isFrozen) {
			MoveH(accel);
			flip = false;
		} else {
			StopH();
		}
		// Move vertically
		if(Input.GetKeyDown(KeyCode.UpArrow) && !isFrozen) {
			MoveV(jumpForce);
		}
		// Flip sprite
		if(flip) {
			transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
		} else {
			transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
		}
		// Swing sword
		if(Input.GetKeyDown(KeyCode.Space)) {
			Animator a = GetComponentInChildren<Animator>();
			a.SetTrigger("SwingTrigger");
		}
	}
	
	public void Freeze() {
		isFrozen = true;
	}
	
	public void Thaw() {
		isFrozen = false;
	}
	
	public void Cripple() {
		maxSpeed /= 2.0f;
	}
	
	private void MoveH(float vel) {
		rb.AddForce(new Vector2(vel, 0.0f));
		if(Mathf.Abs(rb.velocity.x) > maxSpeed) {
			int dir = rb.velocity.x > 0 ? 1 : -1;
			rb.velocity = new Vector2(dir * maxSpeed, rb.velocity.y);
		}
	}
	
	private void StopH() {
		if(rb.velocity.x > 0.0f) {
			rb.AddForce(new Vector2(-decel, 0.0f));
		} else if(rb.velocity.x < 0.0f) {
			rb.AddForce(new Vector2(decel, 0.0f));
		} else {
			rb.velocity = new Vector2(0.0f, rb.velocity.y);
		}
	}
	
	private void MoveV(float vel) {
		if(rb.velocity.y == 0.0f) {
			rb.AddForce(new Vector2(0.0f, vel));
		}
	}
}
