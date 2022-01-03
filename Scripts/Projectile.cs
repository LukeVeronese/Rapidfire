using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public Vector3 normalDirection;
	float speed = 10;

	public Material gray_color;

	private GameObject player;

	private int randNum = 0;

	void Start () {

		player = GameObject.Find ("Player");

		if (gameObject.name == "Player Projectile(Clone)") {
			gameObject.tag = "hit";
			normalDirection = Vector3.forward;
		} 
		else 
		{
			normalDirection = new Vector3(Random.Range(-2f, 2f), 0, -1).normalized;
			speed = Random.Range (5f, 25f);
		}
	}

	void Update () {

		transform.Translate (normalDirection * Time.deltaTime * speed, Space.World);
	}

	void OnCollisionEnter(Collision other) {

		if ((gameObject.CompareTag ("hit") || other.gameObject.name == "Player" || other.gameObject.CompareTag("barrier")) && !other.gameObject.CompareTag ("wall")) {
			Destroy (gameObject);
		}

		if (other.gameObject.CompareTag ("hit")) {
			gameObject.GetComponentInChildren<Renderer> ().material = gray_color;
			gameObject.tag = "hit";
		}

		if (other.gameObject.CompareTag ("wall")) {

			if (gameObject.CompareTag ("hit")) 
			{
				normalDirection = new Vector3 (other.contacts [0].normal.x, 0, normalDirection.z).normalized;
			} 
			else 
			{
				if (transform.position.z >= -2) {
					randNum = Random.Range (1, 8);
				}

				if (randNum == 1) 
				{
					normalDirection = (player.transform.position - transform.position).normalized;
				} 
				else 
				{
					normalDirection = new Vector3 (other.contacts [0].normal.x, 0, normalDirection.z).normalized;
				}
			}
		} else if (other.gameObject.CompareTag ("hit")) {
			normalDirection = new Vector3 (other.contacts [0].normal.x, 0, 1).normalized;
		}
		else {
			normalDirection = new Vector3 (other.contacts [0].normal.x, 0, other.contacts [0].normal.z).normalized;
		}
	}
}