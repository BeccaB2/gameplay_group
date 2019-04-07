using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner_platform_trigger : MonoBehaviour {

	public GameObject platform;
	public GameObject player;

	private float speed = 20.0f;
	Quaternion rotation;

	void Start ()
	{
		
		player = GameObject.FindGameObjectWithTag("Player");
		platform.transform.SetParent (this.transform, false);
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
			player.transform.parent = platform.transform;
		}
	}

	void OnTriggerExit (Collider other)
	{
		player.transform.parent = null;
	}
}
