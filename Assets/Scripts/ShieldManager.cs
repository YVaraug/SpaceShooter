using UnityEngine;
using System.Collections;

public class ShieldManager : MonoBehaviour {

	ParticleSystem shield;
	public Player shieldedObject;

	// Use this for initialization
	void Start () {
		shield = this.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateShield(shieldedObject);
	}

	void UpdateShield(Player shieldedObject){
		if (shieldedObject.shield == null)
			return;
		shield.startSize = (shieldedObject.shield/shieldedObject.maxShield)*0.5f;
		
		
	}
}
