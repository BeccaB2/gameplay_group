using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Moving1_Movement : MonoBehaviour {

	private GameObject player;

	public float speed = 10.0f;
    public bool constant = false;
	public bool platform_move_forward = false;


    private int location = 0;
    private bool hit_check = false;

	public GameObject target0;
	public GameObject target1;
	public GameObject target2;
	public GameObject target3;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");

	}

	// Update is called once per frame
	void Update ()
	{
		float delta_speed = speed * Time.deltaTime;
		MovePlatform (delta_speed);
	}

	private void MovePlatform(float delta_speed)
	{
        if (!hit_check)
        {
            if (platform_move_forward)
            {
                if (location == 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target1.transform.position, delta_speed);
                    if (transform.position == target1.transform.position)
                    {
                        location = 1;
                    }
                }
                if (location == 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target2.transform.position, delta_speed);
                    if (transform.position == target2.transform.position)
                    {
                        location = 2;
                    }
                }
                if (location == 2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target3.transform.position, delta_speed);
                    if (transform.position == target3.transform.position)
                    {
                        location = 3;
                    }
                }
                if (location == 3)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target3.transform.position, delta_speed);
                }
                if (constant && transform.position == target3.transform.position)
                {
                    platform_move_forward = false;
                }
            }

            if (!platform_move_forward)
            {
                if (location == 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target0.transform.position, delta_speed);
                }
                if (location == 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target1.transform.position, delta_speed);
                    if (transform.position == target1.transform.position)
                    {
                        location = 0;
                    }

                }
                if (location == 2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target2.transform.position, delta_speed);
                    if (transform.position == target2.transform.position)
                    {
                        location = 1;
                    }
                }
                if (location == 3)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target3.transform.position, delta_speed);
                    if (transform.position == target3.transform.position)
                    {
                        location = 2;
                    }
                }
                if (constant && transform.position == target0.transform.position)
                {
                    platform_move_forward = true;
                }
            }
        }
	}


	void OnTriggerEnter (Collider other)
	{
        
            if (other.gameObject == player)
            {
                player.transform.parent = this.transform;
            if (!constant)
            {
                if (transform.position == target0.transform.position)
                {
                    StartCoroutine("Platform1Delay");
                }
                else
                {
                    StartCoroutine("Platform2Delay");
                }
            }
		}
	}

	void OnTriggerExit (Collider other)
	{
		player.transform.parent = null;
	}

    public void SetHitCheck(bool hit_player)
    {
        hit_check = hit_player;
    }

    IEnumerator Platform1Delay()
	{
		yield return new WaitForSeconds (0.75f);
		platform_move_forward = true;
	}

	IEnumerator Platform2Delay()
	{
		yield return new WaitForSeconds (0.75f);
		platform_move_forward = false;
	}
}
