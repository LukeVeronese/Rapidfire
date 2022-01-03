using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Volume : MonoBehaviour {

	public GameObject volume_buttonframe;
	public AudioSource audioSource;

	public Texture on;
	public Texture muted;

	void Start () {

        audioSource.Play();

        //Saves volume state

        if (PlayerPrefs.HasKey ("Volume")) {

			audioSource.volume = PlayerPrefs.GetFloat("Volume");

			if (audioSource.volume > 0)
			{
				audioSource.volume = .30f;
				volume_buttonframe.GetComponentInChildren<RawImage>().texture = on;
			}
			else
			{
				volume_buttonframe.GetComponentInChildren<RawImage>().texture = muted;
			}
		}
			
		volume_buttonframe.GetComponent<Button> ().onClick.AddListener (() => {

			if (audioSource.volume > 0)
			{
				audioSource.volume = 0;
				volume_buttonframe.GetComponentInChildren<RawImage>().texture = muted;
			}
			else
			{
				audioSource.volume = .30f;
				volume_buttonframe.GetComponentInChildren<RawImage>().texture = on;
			}

			PlayerPrefs.SetFloat("Volume", audioSource.volume);
		});
	}
}
