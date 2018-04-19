using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public GameObject projectile;

	public float speed;
	public float padding;
	public float firingRate;
	public float health;
	public AudioClip fireSound;

	private float xmin;
	private float xmax;
	//Vector3 topmost;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
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
		LevelManager man = GameObject.Find ("LevelManager").GetComponent <LevelManager> ();
		man.LoadLevel ("Win Screen");
		Destroy (gameObject);
	}

	
	// Update is called once per frame
	void Update () {
		// Move and Position Controll
		if (Input.GetKey (KeyCode.RightArrow)) {
			//transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime; // more readable and simpler than the way above

		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			//transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime; // more readable and simpler than the way above

		}

		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

		// Operates projectiles
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.00001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
	}

	void Fire () {
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity);
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, projectile.GetComponent<Projectile>().speed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}
}
