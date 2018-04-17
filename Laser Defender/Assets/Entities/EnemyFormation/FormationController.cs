using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width;
	public float height;
	public float speed;
	public float spawnDelay;

	private float xmin;
	private float xmax;
	private bool movingRight = true;


	public void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}

	// Use this for initialization
	void Start () {
		// Decides the edges
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));
		xmin = leftBoundary.x;
		xmax = rightBoundary.x;

		SpawnEnemies ();
	}

	// Update is called once per frame
	void Update () {

		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position -= Vector3.right * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + 0.4f * width;
		float leftEdgeOfFormation = transform.position.x - 0.4f * width;
		if (leftEdgeOfFormation < xmin) {
			movingRight = true;
		} else if (rightEdgeOfFormation > xmax) {
			movingRight = false;
		}

		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}

	}

	Transform NextFreePosition () {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;

	}

	// Check if all enemies are dead
	bool AllMembersDead () {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	// Clones enemy for each position object under EnemyFormation
	void SpawnEnemies () {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity); // as GameObject?
			enemy.transform.parent = child; 
		}
	}

	//
	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.transform.position, Quaternion.identity); // as GameObject?
			enemy.transform.parent = freePosition; 
		} 

		if (NextFreePosition ()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
}
