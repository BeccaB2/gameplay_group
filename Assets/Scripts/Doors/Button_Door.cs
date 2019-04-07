using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Door: MonoBehaviour {

    //Button Materials
    public Material m_pressed;

    //doors
    public GameObject door_left;
	public GameObject door_right;
	
    //doors move to here
	public GameObject door_left_target;
	public GameObject door_right_target;

   
    public GameObject button;
	public GameObject button_target;
    public GameObject player_target;

    //cameras
    public GameObject main_camera;

	private GameObject player;

	private bool interaction = false;
	private bool move_doors = false;
	private bool move_button = false;
	private bool triggered = false;

	private float door_speed = 7.5f;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void FixedUpdate()
	{
		interaction = Input.GetButton("Attack1");
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player)
		{
			Debug.Log ("Player Enter Door Trigger");
		}

		if (other.gameObject == player && interaction && !triggered)
		{
            float desiredAngle = transform.eulerAngles.y;
            //player_character.GetComponent<Character_Movement>().SetDisabled (true);
            player.transform.position = new Vector3(player_target.transform.position.x, player.transform.position.y, player_target.transform.position.z);
            player.transform.rotation = Quaternion.Euler(0, desiredAngle, 0);
            //main_camera.GetComponent<MainCamController>().SetDoorCam(1);
            StartCoroutine("ButtonDelay");
			triggered = true;
        }
	}

	void Update()
	{
		float delta_speed = door_speed * Time.deltaTime;

        MoveButton();
        MoveDoors(delta_speed);
	}

    private void MoveButton()
    {
        if (move_button)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, button_target.transform.position, 0.3f * Time.deltaTime);

            if (button.transform.position == button_target.transform.position)
            {
                //	main_camera.GetComponent<MainCamController>().SetDoorCam(2);
                this.GetComponent<MeshRenderer>().material = m_pressed;
                StartCoroutine("DoorDelay");
                move_button = false;
            }
        }
    }

    private void MoveDoors(float delta_speed)
    {
        if (move_doors)
        {
            door_left.transform.position = Vector3.MoveTowards(door_left.transform.position, door_left_target.transform.position, delta_speed);
            door_right.transform.position = Vector3.MoveTowards(door_right.transform.position, door_right_target.transform.position, delta_speed);

            if (door_left.transform.position == door_left_target.transform.position && door_right.transform.position == door_right_target.transform.position)
            {
                StartCoroutine("PlayerRestartDelay");
                move_doors = false;
            }
        }
    }

	IEnumerator ButtonDelay()
	{
		yield return new WaitForSeconds (0.075f);
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
