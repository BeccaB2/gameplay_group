using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner_platform_movement : MonoBehaviour {

	private float speed = 20.0f;
	Quaternion rotation;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float rot_speed = speed * Time.deltaTime;

		transform.Rotate (new Vector3 (0.0f, -rot_speed, 0.0f));
	}
}
