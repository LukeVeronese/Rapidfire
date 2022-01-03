using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public Transform prefab;

	private float spawnRate = 1.0F;
	private float nextSpawn = 0.0F;

	Transform enemy;

	public Material red_color;
	public Material orange_color;
	public Material yellow_color;
	public Material green_color;
	public Material blue_color;
	public Material purple_color;

	public GameObject time;

	public List<Transform> enemies = new List<Transform>();

	public int min = 0;
	public int sec = 0;
	float nextsec;

	public int time_score = 0;
	public int kill_score = 0;
	public string game_status = "started";

	private float spawnRateMin;
	private float spawnRateMax;

	public Transform scoreUI;
	public Transform timeUI;
	public Transform livesUI;
	public Transform volumeUI;

	public Transform timeScoreUI;
	public Transform killScoreUI;
	public Transform totalScoreUI;
	public Transform highestScoreUI;

	public Transform leftUI;
	public Transform rightUI;
	public Transform playAgain;
	public Transform menu;

	void Start () {

		Material[] colors = { red_color, orange_color, yellow_color, green_color, blue_color, purple_color };

		int index = Random.Range (0, 5);
		enemy = Instantiate (prefab, new Vector3 (Random.Range (-7.0f, 7.0f), 0.5f, 10.0f), Quaternion.identity);
		enemy.GetComponentInChildren<Renderer> ().material = colors [index];
		enemies.Add (enemy);

		index = Random.Range (0, 5);
		enemy = Instantiate (prefab, new Vector3 (Random.Range (-7.0f, 7.0f), 0.5f, 10.0f), Quaternion.identity);
		enemy.GetComponentInChildren<Renderer> ().material = colors [index];
		enemies.Add (enemy);

		scoreUI.position = new Vector2 (Screen.width * .231f, Screen.height * .941f);
		timeUI.position = new Vector2 (Screen.width / 2, Screen.height * .941f);
		livesUI.position = new Vector2 (Screen.width * .774f, Screen.height * .941f);
		volumeUI.position = new Vector2 (Screen.width * .928f, Screen.height * .941f);

		timeScoreUI.position = new Vector2 (Screen.width / 2, Screen.height * .813f);
		killScoreUI.position = new Vector2 (Screen.width / 2, Screen.height * .724f);
		totalScoreUI.position = new Vector2 (Screen.width / 2, Screen.height * .635f);
		highestScoreUI.position = new Vector2 (Screen.width / 2, Screen.height * .546f);

		leftUI.position = new Vector2 (Screen.width * .181f, Screen.height * .166f);
		rightUI.position = new Vector2 (Screen.width * .819f, Screen.height * .166f);
		playAgain.position = new Vector2 (Screen.width / 2, Screen.height * .367f);
		menu.position = new Vector2 (Screen.width / 2, Screen.height * .16f);
	}

	void Update () {

		if (game_status != "ended") {
			
			Timer ();

			Material[] colors = { red_color, orange_color, yellow_color, green_color, blue_color, purple_color };

			if (sec < 20) {
				spawnRateMin = 0.8f;
				spawnRateMax = 1.5f;
			} else if (sec >= 20 && sec < 40) {
				spawnRateMin = 0.5f;
				spawnRateMax = 1.2f;
			} else if (sec >= 40 && sec < 60) {
				spawnRateMin = 0.5f;
				spawnRateMax = 1.0f;
			} else {
				spawnRateMin = 0.5f;
				spawnRateMax = 0.8f;
			}

			if (Time.time > nextSpawn && enemies.ToArray ().Length < 10) {

				nextSpawn = Time.time + spawnRate;
				spawnRate = Random.Range (spawnRateMin, spawnRateMax);

				int index = Random.Range (0, 5);

				enemy = Instantiate (prefab, new Vector3 (Random.Range (-7.0f, 7.0f), 0.5f, 10.0f), Quaternion.identity);
				enemy.GetComponentInChildren<Renderer> ().material = colors [index];

				enemies.Add (enemy);

				if (sec >= 45) {
					index = Random.Range (0, 5);

					enemy = Instantiate (prefab, new Vector3 (Random.Range (-7.0f, 7.0f), 0.5f, 10.0f), Quaternion.identity);
					enemy.GetComponentInChildren<Renderer> ().material = colors [index];

					enemies.Add (enemy);
				}
			}
		}
	}

	void Timer ()
	{
		if (Time.time > nextsec) {

			time_score += 2;

			nextsec = Time.time + 1;

			if (sec == 59) {
				min += 1;
				sec = 0;
			} 
			else 
			{
				sec++;
			}
		}

		if (sec < 10) {
			time.GetComponentInChildren<Text>().text = min.ToString() + ":0" + sec.ToString();
		} 
		else 
		{
			time.GetComponentInChildren<Text>().text = min.ToString() + ":" + sec.ToString();
		} 
	}
}