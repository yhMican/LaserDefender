using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5.0f;
	public float speed = 5.0f;
	public float padding;

	private float xmin;
	private float xmax;
	private bool movingRight = true;


	// Use this for initialization
	void Start () {
		// Decides the edges
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));
		xmin = leftBoundary.x + padding;
		xmax = rightBoundary.x - padding;

		// Clones enemy for each position object under EnemyFormation
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity); // as GameObject?
			enemy.transform.parent = child; 
		}


	}

	public void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}
	// Update is called once per frame
	void Update () {

		// My try
		/*transform.position += Vector3.right * speed * Time.deltaTime;
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		if (newX == xmin || newX == xmax) {
			speed *= -1;
		}*/

		// By Lecture
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position -= Vector3.right * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + 0.5f * width;
		float leftEdgeOfFormation = transform.position.x - 0.5f * width;
		if (leftEdgeOfFormation < xmin || rightEdgeOfFormation > xmax) {
			movingRight = !movingRight;
		}// Can take the width of the Gizmo's width into account.



	}
}
