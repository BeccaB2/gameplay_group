using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner_platform_trigger : MonoBehaviour {

	//public GameObject platform;
	private GameObject player;

	public float speed = 20.0f;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		transform.SetParent (this.transform, false);
	}

	void Update()
	{
		float rot_speed = speed * Time.deltaTime;

		transform.Rotate (new Vector3 (0.0f, -rot_speed, 0.0f));
	}

	void OnTriggerEnter (Collider other)
	{

		if (other.gameObject == player)
		{
			player.transform.parent = transform;
		}
	}

	void OnTriggerExit (Collider other)
	{
		player.transform.parent = null;
	}
}
