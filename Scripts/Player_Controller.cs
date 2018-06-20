using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour {
	public GameObject player;
	public float velocity = 2.0f;
	private Rigidbody2D rb;
	void Start(){
		rb = GetComponent<Rigidbody2D> ();
	}
	void FixedUpdate(){
		float move_Horizontal = Input.GetAxis ("Horizontal");
		float move_Vertical;
		move_Vertical = Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow) ? move_Vertical = Input.GetAxis ("Vertical") : move_Vertical = 0.0f;
		Vector2 move = new Vector2 (move_Horizontal, move_Vertical);
		rb.velocity = move*velocity;
		rb.AddForce (Vector2.left * move_Horizontal * Time.deltaTime);
		rb.AddForce (Vector2.up * move_Vertical * Time.deltaTime);
}
}
