using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Transform prefab;

	private float fireRate = 1.0F;
	private float nextFire = 0.0F;

	private float moveRate = 1.0F;
	private float nextMove = 0.0F;

	float speed = 5;

	Transform projectile;
	Vector3 direction;

	Manager manager;

	void Start () {

		manager = FindObjectOfType<Manager>();

		direction = new Vector3 (Random.Range (-1f, 1f), 0, 0).normalized;
	}

	void Update () {

        if (manager.game_status == "ended")
        {
            Destroy(gameObject);
        }

		transform.Translate (direction * Time.deltaTime * speed);

		if (transform.position.x > 8.5) {

			direction = new Vector3(-1f, 0, 0);
		}

		if (transform.position.x < -8.5) {

			direction = new Vector3(1f, 0, 0);
		}
		
		if (Time.time > nextFire) {

			nextFire = Time.time + fireRate;
			fireRate = Random.Range(0.5f, 1.0f);

			projectile = Instantiate (prefab, transform.position + Vector3.back * 2, transform.rotation);
			projectile.GetComponentInChildren<Renderer> ().material = gameObject.GetComponentInChildren<Renderer>().material;
		}

		if (Time.time > nextMove) {

			nextMove = Time.time + moveRate;
			moveRate = Random.Range(1.0f, 3.0f);

			speed = Random.Range (5.0f, 15.0f);
			direction = new Vector3 (Random.Range (-1f, 1f), 0, 0).normalized;
		}
	}

	void OnCollisionEnter(Collision other) {
		
		if (other.gameObject.CompareTag ("hit")) {

			manager.kill_score += 5;
			manager.enemies.Remove (transform);
			manager.enemies.TrimExcess ();
			Destroy (gameObject);
		}
	}
}