using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float damage;
	public float speed;

	public float GetDamage () {
		return damage;
	}

	public void Hit () {
		Destroy (gameObject); // self-destruction
	}

}
