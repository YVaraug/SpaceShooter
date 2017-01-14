using UnityEngine;
using System.Collections;

public class AsteroidDestroyer : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Asteroid")			
			Destroy (other.gameObject);
	}

}
