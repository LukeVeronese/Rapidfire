using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

	public Transform prefab;

	private float fireRate = 0.5F;
	private float nextFire = 0.0F;

	Transform projectile;

	int lives = 3;
	const float speed = 12;

	Manager manager;

	public GameObject playAgain_buttonframe;
	public GameObject menu_buttonframe;
	public GameObject canvas;

	public Text total_scoreText;
	public Text time_scoreText;
	public Text kill_scoreText;
	public Text highest_scoreText;

	public Text livesText;
	public Text scoreText;

 	bool goingLeft = false;
	bool goingRight = false;

	private int total_score = 0;
	private int highest_score = 0;

    public AudioSource audioSource;

    void Start () {

        if (PlayerPrefs.HasKey ("Highscore")) {

			highest_score = PlayerPrefs.GetInt ("Highscore");
		}

		manager = FindObjectOfType<Manager>();

		playAgain_buttonframe.GetComponent<Button> ().onClick.AddListener (() => {
            audioSource.Play();
			SceneManager.LoadScene(1);
		});

		menu_buttonframe.GetComponent<Button> ().onClick.AddListener (() => {
			SceneManager.LoadScene(0);
		});
	}

	public void LeftButtonDown() {
		goingLeft = true;
	}

	public void LeftButtonUp() {
		goingLeft = false;
	}

	public void RightButtonDown() {
		goingRight = true;
	}

	public void RightButtonUp() {
		goingRight = false;
	}
		
	void Update () {

		if (goingLeft == true) {
			
			if (transform.position.x > -7f) {
				
				transform.Translate (Vector3.left * Time.deltaTime * speed);
			}
		}

		if (goingRight == true) {

			if (transform.position.x < 7f) {

				transform.Translate (Vector3.right * Time.deltaTime * speed);
			}
		}
			
		if (manager.game_status != "ended") {
			
			livesText.text = "Lives: " + lives.ToString ();
			scoreText.text = "Score: " + (manager.time_score + manager.kill_score).ToString ();
		}

		if (Time.time > nextFire && manager.game_status != "ended") {

			nextFire = Time.time + fireRate;
			projectile = Instantiate (prefab, transform.position + Vector3.forward * 2, transform.rotation);
		}
	}

	void OnCollisionEnter(Collision other) {

		if (!other.gameObject.CompareTag ("hit")) {
			
			lives--;

			livesText.text = "Lives: " + lives.ToString ();

			if (lives <= 0) {

                audioSource.Stop();
                manager.game_status = "ended";

				canvas.GetComponent<Image> ().enabled = true;

				playAgain_buttonframe.GetComponent<Button> ().enabled = true;
				playAgain_buttonframe.GetComponent<Image> ().enabled = true;
				playAgain_buttonframe.GetComponentInChildren<Text> ().enabled = true;

				menu_buttonframe.GetComponent<Button> ().enabled = true;
				menu_buttonframe.GetComponent<Image> ().enabled = true;
				menu_buttonframe.GetComponentInChildren<Text> ().enabled = true;

				total_score = manager.time_score + manager.kill_score;

				total_scoreText.enabled = true;
				total_scoreText.text = "Total score: " + total_score.ToString ();

				time_scoreText.enabled = true;
				time_scoreText.text = "Time score: " + manager.time_score.ToString ();

				kill_scoreText.enabled = true;
				kill_scoreText.text = "Kill score: " + manager.kill_score.ToString ();

				if (total_score > highest_score) 
				{
					PlayerPrefs.SetInt ("Highscore", total_score);
					highest_scoreText.text = "Highest score: " + total_score.ToString ();
				} 
				else 
				{
					highest_scoreText.text = "Highest score: " + highest_score.ToString ();
				}

				highest_scoreText.enabled = true;

				gameObject.GetComponent<MeshRenderer> ().enabled = false;
				gameObject.GetComponent<Collider> ().enabled = false;
			}
		}
	}
}