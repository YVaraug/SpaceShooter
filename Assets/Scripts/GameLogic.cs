using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

	//To Spawn the enemy
	public GameObject enemyPrefab;
	float spawnDistance = 20f;
	float enemyRate = 20;
	float timeTillNextEnemy = 10;
	public float firstWaveEnemyNumbers = 5;

	//Wave number
	public Text waveNumberText;
	public int waveNumber = 1;

	//Resources
	public Text resourcesText;
	public int resources = 0;

	//To Spawn Asteroids
	private float screenHeight;
	public GameObject[] asteroids ;

	public GameObject shopMenu;

	void Start () {
		
		screenHeight = Camera.main.orthographicSize;
		//Populate the screen with initial asteroids
		for (int i = 0; i < asteroids.Length; i++)
		{
			SpawnObjectsRandomly(asteroids[i], Random.Range(2,5), 5,
				new Vector3(Random.Range(-10,10),Random.Range(-screenHeight,screenHeight),0));
		}
	}
	

	void Update () {
	
		timeTillNextEnemy -= Time.deltaTime;
		resourcesText.text = resources.ToString();
		//New Wave
		if(timeTillNextEnemy <= 0) 
		{
			ToggleGameObject(shopMenu);
			//We update the text
			waveNumber++;
			waveNumberText.text = "Wave: " + waveNumber.ToString();
			//Spawn enemies
			timeTillNextEnemy = enemyRate;
			SpawnObjectsRandomly(enemyPrefab, firstWaveEnemyNumbers, spawnDistance, transform.position);
			//Spawn asteroids
			for (int i = 0; i < asteroids.Length; i++)
			{
				SpawnObjectsRandomly(asteroids[i], Random.Range(0,3), 5,
					                 new Vector3(-28,Random.Range(-screenHeight,screenHeight),0));
			}

			firstWaveEnemyNumbers++;
		}

		if(GameObject.FindGameObjectWithTag("Player") == null)

		{
			SceneManager.LoadScene ("MainMenu");
		}

	}

	public void ToggleGameObject(GameObject GO){

		if (GO.activeSelf) {
			
			Time.timeScale = 1;
		} else {
			Time.timeScale = 0;
		}
		
		GO.SetActive (!GO.activeSelf);
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

	void SpawnObjectsRandomly(GameObject objectToSpawn,float number, float distance, Vector3 pos)
	{
		//Spawns a number of prefabs at a distance between 
		//0.75 and 1.25 of the marked position

		for (int i = 0; i < number; i++)
		{
			Vector3 offset = Random.onUnitSphere;
			offset.z = 0;
			offset = offset.normalized * distance * Random.Range(0.75f,1.25f);
			Instantiate(objectToSpawn, pos + offset, Quaternion.identity);
		}


	}

	void ZoomInOut(int maxOrthoSize = 20, int minOrthoSize = 5)
	{
		// Gets the input of the Scroll Wheel
		float scrollButton = Input.mouseScrollDelta.y;
		// Gets the actual Ortho Size
		float orthoSize = Camera.main.orthographicSize;
		// Zooms in or out till a Max or Min
		if (scrollButton > 0 && orthoSize < maxOrthoSize) {
			Camera.main.orthographicSize++;
		} 
		else if (scrollButton < 0 && orthoSize > minOrthoSize) {
			Camera.main.orthographicSize--;
		}
	}
}
