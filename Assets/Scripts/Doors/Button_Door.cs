using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Door: MonoBehaviour {

	//doors
	public GameObject door_left;
	public GameObject door_right;
	//doors move to here
	public GameObject door_left_target;
	public GameObject door_right_target;

	public GameObject button;
	public GameObject button_trigger;
	public GameObject main_camera;

	private GameObject player_character;

	private bool interaction = false;
	private bool move_doors = false;
	private bool move_button = false;
	private bool triggered = false;

	private float button_pos_z = 0;
	private float door_speed = 10f;

	void Start ()
	{
		player_character = GameObject.FindGameObjectWithTag("Player");
	}
	
	void FixedUpdate()
	{
		interaction = Input.GetButton("Interaction");
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player_character)
		{
			Debug.Log ("Player Enter Door Trigger");
		}

		if (other.gameObject == player_character && interaction && !triggered)
		{
			//player_character.GetComponent<Character_Movement>().SetInteraction (true);
			//player_character.GetComponent<Character_Movement>().SetDisabled (true);
			player_character.transform.position = new Vector3 (button_trigger.transform.position.x, 0.05f, button_trigger.transform.position.z);
			player_character.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			//main_camera.GetComponent<MainCamController>().SetDoorCam(1);
			button_pos_z = button.transform.position.z + 0.05f;
			StartCoroutine("ButtonDelay");
			triggered = true;
		}
	}

	void Update()
	{
		float delta_speed = door_speed * Time.deltaTime;

		if (move_doors)
		{
			door_left.transform.position = Vector3.MoveTowards (transform.position, door_left_target.transform.position, delta_speed);
			door_right.transform.position = Vector3.MoveTowards (transform.position, door_right_target.transform.position, delta_speed);

			if (door_left.transform.position == door_left_target.transform.position && door_right.transform.position == door_right_target.transform.position)
			{
				StartCoroutine("PlayerRestartDelay");
				move_doors = false;
			}
		}

		if (move_button)
		{
			button.transform.position = new Vector3 (button.transform.position.x, button.transform.position.y ,button.transform.position.z + 0.1f * Time.deltaTime);

			if (button.transform.position.z >= button_pos_z)
			{
			//	main_camera.GetComponent<MainCamController>().SetDoorCam(2);
				StartCoroutine("DoorDelay");
				move_button = false;
			}
		}
	}

	IEnumerator ButtonDelay()
	{
		yield return new WaitForSeconds (0.05f);
		move_button = true;
	}

	IEnumerator DoorDelay()
	{
		yield return new WaitForSeconds (0.5f);
		move_doors = true;
	}

	IEnumerator PlayerRestartDelay()
	{
		yield return new WaitForSeconds (1f);
		//main_camera.GetComponent<MainCamController> ().CameraReset();
		//player_character.GetComponent<Character_Movement>().SetInteraction (false);
		//player_character.GetComponent<Character_Movement>().SetDisabled (false);
	}
}
