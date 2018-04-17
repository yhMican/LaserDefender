using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public GameObject projectile;
	public float health;
	public float firingRate;
	public float ShotsPerSeconds;

	void Update () {
		float probability = Time.deltaTime * ShotsPerSeconds;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		//Debug.Log (col);
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Destroy (gameObject);
			}
		}
	}
		
	void Fire () {
		Vector3 startPosition = transform.position + new Vector3 (0, -1, 0);
		GameObject beam = Instantiate (projectile, startPosition, Quaternion.identity);
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -projectile.GetComponent<Projectile>().speed, 0);
	}

}
