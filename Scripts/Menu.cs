using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject play_buttonframe;
	public Text highest_scoreText;

	public Transform title;
	public Transform highestScore;
	public Transform volume;

	public Transform cubesUpperL;
	public Transform cubesUpperR;
	public Transform cubesLowerL;
	public Transform cubesLowerR;

	void Start () {

		title.position = new Vector2 (Screen.width / 2, Screen.height * .86f); 
		highestScore.position = new Vector2 (Screen.width / 2, Screen.height * .135f); 
		volume.position = new Vector2 (Screen.width * .89f, Screen.height * .85f); 

		cubesUpperL.position = new Vector2 (Screen.width * .096f, Screen.height * .918f); 
		cubesUpperR.position = new Vector2 (Screen.width * .888f, Screen.height * 1.014f); 
		cubesLowerL.position = new Vector2 (Screen.width * .1175f, Screen.height * -.0121f); 
		cubesLowerR.position = new Vector2 (Screen.width * .905f, Screen.height * .091f); 

		if (PlayerPrefs.HasKey ("Highscore")) {

			highest_scoreText.text = "Highest score: " + PlayerPrefs.GetInt ("Highscore").ToString();
		}

		play_buttonframe.GetComponent<Button> ().onClick.AddListener (() => {
			SceneManager.LoadScene(1);
		});
	}
}