using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HUDController : MonoBehaviour {

	//Gets the slider GameObjects and the player
	public Slider healthSlider; 
	public Slider shieldSlider;
	public Player player;

	void Update () {
		
		//Updates HUD values
		healthSlider.value = player.health;
		healthSlider.maxValue = player.maxHealth;
		shieldSlider.value = player.shield;
		shieldSlider.maxValue = player.maxShield;


	
	}
}
