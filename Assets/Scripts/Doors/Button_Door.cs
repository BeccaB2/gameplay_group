using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Door: MonoBehaviour {

    //Button Materials
    public Material m_Red;
    public Material m_Green;

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
    public GameObject key_camera;
    public GameObject door_camera;

    private GameObject player;

	private bool interaction = false;
	private bool move_doors = false;
	private bool move_button = false;
    private bool trigger_key = false;
	private bool triggered = false;

	private float door_speed = 7.5f;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void FixedUpdate()
	{
		interaction = Input.GetButton("Attack1") || Input.GetKeyDown(KeyCode.Joystick1Button2);
    }

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log ("Player Enter Door Trigger");
		}

        if (other.tag == "Player" && interaction && characterControls.keyCollected == false && !trigger_key)
        {
            main_camera.SetActive(false);
            key_camera.SetActive(true);
            trigger_key = true;
            StartCoroutine("CamDelay");
        }

            if (other.tag == "Player" && interaction && !triggered && characterControls.keyCollected == true)
		{
            float desiredAngle = transform.eulerAngles.y;
            other.GetComponent<characterControls>().canInput = false;
            player.transform.position = new Vector3(player_target.transform.position.x, player.transform.position.y, player_target.transform.position.z);
            player.transform.rotation = Quaternion.Euler(0, desiredAngle, 0);
            DoorCam();
            StartCoroutine("ButtonDelay");
            triggered = true;
        }
	}

	void Update()
	{
		float delta_speed = door_speed * Time.deltaTime;

        MoveButton();
        MoveDoors(delta_speed);

        if (characterControls.keyCollected == true && !triggered)
        {
            SetColourGreen();
        }
    }

    private void MoveButton()
    {
        if (move_button)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, button_target.transform.position, 0.3f * Time.deltaTime);

            if (button.transform.position == button_target.transform.position)
            {
                SetColourRed();
                StartCoroutine("DoorDelay");
                move_button = false;
            }

            player.transform.position = new Vector3(player_target.transform.position.x, player.transform.position.y, player_target.transform.position.z);
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
                player.GetComponent<characterControls>().canInput = true;
                move_doors = false;
            }

            player.transform.position = new Vector3(player_target.transform.position.x, player.transform.position.y, player_target.transform.position.z);
        }
    }

    public void SetColourRed()
    {
        this.GetComponent<MeshRenderer>().material = m_Red;
    }

    public void SetColourGreen()
    {
        this.GetComponent<MeshRenderer>().material = m_Green;
    }

    public void DoorCam()
    {
        main_camera.SetActive(false);
        door_camera.SetActive(true);

        door_camera.GetComponent<DoorCam_Controls>().setDoorCamActive();
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
    }
    IEnumerator CamDelay()
    {
        yield return new WaitForSeconds(1.5f);
        key_camera.SetActive(false);
        main_camera.SetActive(true);
        trigger_key = false;
    }
}
