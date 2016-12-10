using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

	//To Spawn the enemy
	public GameObject enemyPrefab;
	float spawnDistance = 12f;
	float enemyRate = 15;
	float nextEnemy = 1;
	float firstWaveEnemyNumbers = 5;

	//To Spawn Player
	//public GameObject playerPrefab;


	void Start () {

		//Needs to be called on the image
		//ImageScaler (); 
		//We will start with the player already in the world
		//Instantiate(playerPrefab, transform.position, Quaternion.identity);
	
	}
	

	void Update () {
	
		nextEnemy -= Time.deltaTime;
	
		if(nextEnemy <= 0) {
			nextEnemy = enemyRate;
			SpawnObjectsRandomly(enemyPrefab, firstWaveEnemyNumbers, spawnDistance);
			firstWaveEnemyNumbers++;
		}

		if(GameObject.FindGameObjectWithTag("Player") == null)

		{
			SceneManager.LoadScene ("MainMenu");
		}

	}

	void ImageScaler()
	{
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

	void SpawnObjectsRandomly(GameObject objectToSpawn,float number, float distance)
	{

		for (int i = 0; i < number; i++)
		{
			Vector3 offset = Random.onUnitSphere;
			offset.z = 0;
			offset = offset.normalized * distance * Random.Range(0.5f,2);
			Instantiate(objectToSpawn, transform.position + offset, Quaternion.identity);
		}


	}
}
