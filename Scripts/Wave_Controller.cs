using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class Wave_Controller : MonoBehaviour {
	public float radius = 2;
	public float radiusLimit = 15.0f;
	public int segments = 20; 
	public float angle = 30f;
	public float factorWidth = 0.13f;
	public float speed = 1f;
	public float duringTime = 5.0f;
	public float timingWave = 4f;
	public GameObject[] enemy;
	private bool hasCollided;
	private int count_Fire;
	private float time_Start;
	private float timer;
	[SerializeField] public List<GameObject> collided;
	// Use this for initialization
	void Start () {
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer> ();
		CircleCollider2D col = gameObject.AddComponent<CircleCollider2D> ();
		List<GameObject> collided = new List<GameObject> ();
		lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
		lineRenderer.widthMultiplier = factorWidth;
		lineRenderer.positionCount = segments + 1; 
		lineRenderer.useWorldSpace = true; 
		lineRenderer.loop = true;
		lineRenderer.alignment = LineAlignment.View;
		Vector3 start_Pos = lineRenderer.transform.localPosition;
		count_Fire = 0;
		foreach (GameObject enemys in enemy) {
			enemys.GetComponent<Renderer> ().enabled = false;
		}
	}
	void Update(){
		LineRenderer lineRenderer = GetComponent<LineRenderer> ();
		if (Input.GetKeyDown (KeyCode.Mouse0) && count_Fire == 0) {
			time_Start = Time.time;
			InvokeRepeating("CrescentWave", duringTime,0.01f);	
			count_Fire++;
		}
		if (Timer (timingWave)) {
			CancelInvoke ("CrescentWave");
			count_Fire = 0;
			radius = 0;
		}
	}
	void OnCollisionEnter2D(Collision2D coll) {
		foreach (GameObject enemys in enemy) {
			if (coll.gameObject.name == enemys.name) {
				hasCollided = true;
				collided.Add (enemys);
			}
		}
	}
	void OnCollisionExit2D(Collision2D coll) {
		foreach (GameObject enemys in enemy) {
			if (coll.gameObject.name == enemys.name) {
				hasCollided = false;
			}
		}
	}
	bool Timer(float timing){
		timer = Time.time - time_Start;
		bool finished;
		if (timer >= timing) {
			finished = true;
		} else {
			finished = false;
		}
		return finished;
	}
	void CrescentWave(){
		float x;
		float y;
		float z = 0.0f;
		var t = (Time.deltaTime * speed) / segments;
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		CircleCollider2D col = GetComponent<CircleCollider2D> ();
		col.gameObject.transform.position = lineRenderer.transform.position;
		col.radius = radius;
		col.transform.parent = lineRenderer.transform;
		for (int i = 0; i < segments + 1; i++) {
			radius = (radius < radiusLimit) ? radius += t/20 : radius = 0;
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius; 
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;
			lineRenderer.SetPosition (i, new Vector3 (x, y, z));
			angle += 360f / segments;
			}
		foreach (GameObject inimigos_coll in collided) {
			if (hasCollided == true) {
				inimigos_coll.GetComponent<Renderer> ().enabled = true;
			} else {
				inimigos_coll.GetComponent<Renderer> ().enabled = false;
			}
		}
	}
}
