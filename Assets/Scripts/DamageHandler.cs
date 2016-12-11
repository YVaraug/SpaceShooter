using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DamageHandler : MonoBehaviour {

	public int health = 1;
	public Slider playerHP;

	void Start() {

	}

	void OnTriggerEnter2D() {
		health--;
		if (playerHP) {
			playerHP.value = health;
		}
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

	public void Health ()
	{

	}

}
