using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.ComponentModel.Design.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class ShopScript : MonoBehaviour {

	[SerializeField]
	GameLogic gamelogic;
	[SerializeField]
	Player player;
	public Text insufResources;
	public int cost = 50;

	void Start(){
		insufResources.enabled = false;
	}

	public void AddSkillPoints(Slider slider)
	{
		if (ConsumeResources (slider) & slider.value < slider.maxValue){
			insufResources.enabled = false;
			UpdateSlider (slider, 1);
			UpdatePlayer (slider);
		}else{
			insufResources.enabled = true;
		}

	}

	void UpdateSlider(Slider slider, float amount, bool additive = true)
	{
		if (additive)
		{
			slider.value += amount;
		}
		else
		{
			slider.value *= amount;
		}
	}

	bool ConsumeResources(Slider slider){

		if (cost * (slider.value +1) > gamelogic.resources) {
				return false;
			} else {
			gamelogic.resources -= cost * ((int)slider.value + 1) ;
				return true;
			}
	}

	public void UpdatePlayer(Slider slider){
		if (slider.name == "HullPointsSlider")
		{
			player.maxHealth++;
			player.health++;
		}
		if (slider.name == "ShieldSlider")
		{
			player.maxShield++;
			player.shield++;
		}
		if (slider.name == "ShieldRegenSlider")
		{
			player.regenRate += 0.1f;
		}
		if (slider.name == "SpeedSlider")
		{
			player.maxSpeed *= 1.05f;
		}
		if (slider.name == "TurningSlider")
		{
			player.rotSpeed *= 1.05f;
		}
		if (slider.name == "FireSlider")
		{
			player.fireDelay *= 0.9f;
		}

	}


}
