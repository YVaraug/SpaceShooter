using UnityEngine;
using System.Collections;

public class BGScaler : MonoBehaviour {

//	float timer = 1f;
//	float refreshRate = 1f;

	void Start () {
	
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		Vector3 tempScale = transform.localScale;

		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;
		float worldHight = Camera.main.orthographicSize * 2f;
		float worldWidth = (worldHight / Screen.height) * Screen.width;

		tempScale.x = worldWidth / width;
		tempScale.y = worldHight / height;
		transform.localScale = tempScale;
	}

	void Update(){
	
//		timer -= Time.deltaTime;
//
//		if (timer <= 0) 
//		{
//			SpriteRenderer sr = GetComponent<SpriteRenderer> ();
//			Vector3 tempScale = transform.localScale;
//			float width = sr.sprite.bounds.size.x;
//			float height = sr.sprite.bounds.size.y;
//			float worldHight = Camera.main.orthographicSize * 2f;
//			float worldWidth = (worldHight / Screen.height) * Screen.width;
//
//			tempScale.x = worldWidth / width;
//			tempScale.y = worldHight / height;
//			transform.localScale = tempScale;
//			timer = refreshRate;
//		}
	}
		
}
	