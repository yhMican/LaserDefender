using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {

	public GameObject projectile;
	public float health;
	public float firingRate;
	public float ShotsPerSeconds;
	public int scoreValue;
	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;

	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

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
				Die ();
			}
		}
	}
		
	void Die () {
		AudioSource.PlayClipAtPoint (deathSound, transform.position);
		Destroy (gameObject);
		scoreKeeper.Score (scoreValue);
	}

	void Fire () {
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity);
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -projectile.GetComponent<Projectile>().speed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

}
