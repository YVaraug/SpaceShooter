using UnityEngine;
using System.Collections;

public class DamageHandler : MonoBehaviour {

	public int health = 1;


	SpriteRenderer spriteRend;

	void Start() {

	}

	void OnTriggerEnter2D() {
		health--;
	}

	void Update() {
		if (health <= 0) 
		{
			Die ();
		}
	}


	void Die() {
		Destroy(gameObject);
	}

}
