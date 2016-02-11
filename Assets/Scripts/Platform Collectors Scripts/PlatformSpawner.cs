using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] platforms;

	private float distanceBetweenPlatforms = 3f;
	private float minX, maxX;
	private float lastPlatformPositionY;
	private float controlX;

	[SerializeField]
	private GameObject[] collectables;

	private GameObject player;


	void Awake () {
		controlX = 0;
		SetMinAndMaxX ();
		CreatePlatforms ();
		player = GameObject.Find ("Player");
	}

	void Start () {
		PositionThePlayer ();
	}

	void SetMinAndMaxX() {
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width,Screen.height,0));

		maxX = bounds.x - 0.5f;
		minX = -bounds.x + 0.5f;			
	}

	void Shuffle (GameObject[] arrayToshuffle) {
		for( int i = 0; i < arrayToshuffle.Length; i++ ) {
			GameObject temp = arrayToshuffle[i];
			int random = Random.Range(i, arrayToshuffle.Length);
			arrayToshuffle[i] = arrayToshuffle[random];
			arrayToshuffle[random] = temp;
		}
	}

	void CreatePlatforms () {

		Shuffle (platforms);

		float positionY = 0f;

		for(int i = 0; i < platforms.Length; i++) {

			Vector3 temp = platforms[i].transform.position;

			temp.y = positionY;

			if (controlX == 0) {
				temp.x = Random.Range (0.0f, maxX);
				controlX = 1;
			} else if (controlX == 1) {
				temp.x = Random.Range (0.0f, minX);
				controlX = 2;			
			} else if (controlX == 2) {
				temp.x = Random.Range (1.0f, maxX);
				controlX = 3;
			} else if (controlX == 3) {
				temp.x = Random.Range (-1.0f, minX);
				controlX = 0;
			}

			lastPlatformPositionY = positionY;

			platforms[i].transform.position = temp;

			positionY -= distanceBetweenPlatforms;

		}

	}

	void PositionThePlayer () {

		GameObject[] spikes = GameObject.FindGameObjectsWithTag ("Deadly");
		GameObject[] platformsInGame = GameObject.FindGameObjectsWithTag ("Platform");

		for (int i = 0; i < spikes.Length; i++) {

			if (spikes[i].transform.position.y == 0f) {

				Vector3 t = spikes[i].transform.position;

				spikes[i].transform.position = new Vector3 (platformsInGame[0].transform.position.x,
															   platformsInGame[0].transform.position.y,
					                                           platformsInGame[0].transform.position.z);

				platformsInGame[0].transform.position = t;
																			
			}

		} 

		Vector3 temp = platformsInGame[0].transform.position;

		for (int i = 1; i < platformsInGame.Length; i++) {
			if (temp.y < platformsInGame[i].transform.position.y) {
				temp = platformsInGame[i].transform.position;
			}
		}

		temp.y += 0.8f;

		player.transform.position = temp;
	}

	void OnTriggerEnter2D(Collider2D target) {

		if (target.tag == "Platform" || target.tag == "Deadly") {

			if (target.transform.position.y == lastPlatformPositionY) {

				Shuffle(platforms);
				Shuffle(collectables);

				Vector3 temp = target.transform.position;

				for (int i = 0; i < platforms.Length; i++) {
				
					if (!platforms[i].activeInHierarchy) {
						
						if (controlX == 0) {
							temp.x = Random.Range (0.0f, maxX);
							controlX = 1;
						} else if (controlX == 1) {
							temp.x = Random.Range (0.0f, minX);
							controlX = 2;			
						} else if (controlX == 2) {
							temp.x = Random.Range (1.0f, maxX);
							controlX = 3;
						} else if (controlX == 3) {
							temp.x = Random.Range (-1.0f, minX);
							controlX = 0;
						}

						temp.y -= distanceBetweenPlatforms;

						lastPlatformPositionY = temp.y;

						platforms[i].transform.position = temp;
						platforms[i].SetActive (true);

					}
				}
			}
		}
	}
}
