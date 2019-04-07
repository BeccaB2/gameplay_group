using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Lift1_Movement : MonoBehaviour {

	public GameObject start_point;
	public GameObject end_point;

	private GameObject player;

    public bool disable_parent = false;
    private bool hit_check = false;
	public float speed = 10.0f;
	private bool platform_move = false;

	// Use this for initialization
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
            if (platform_move)
            {
                transform.position = Vector3.MoveTowards(transform.position, end_point.transform.position, delta_speed);
            }

            if (!platform_move)
            {
                transform.position = Vector3.MoveTowards(transform.position, start_point.transform.position, delta_speed);
            }
        }
	}

	void OnTriggerEnter (Collider other)
	{
		
		if (other.gameObject == player && !disable_parent)
		{
            Debug.Log("Player Is on Lift");
			player.transform.parent = this.transform;
			StartCoroutine("PlatformDelay");
		}

        if (other.gameObject == player && disable_parent)
        {
            Debug.Log("Player in collider");
            StartCoroutine("PlatformDelay");
        }
    }

	void OnTriggerExit (Collider other)
	{
        if (other.gameObject == player)
        {
            player.transform.parent = null;
            StartCoroutine("PlatformBackDelay");
        }
	}

    public void SetHitCheck(bool hit_player)
    {
        hit_check = hit_player;
    }

	IEnumerator PlatformDelay()
	{
		yield return new WaitForSeconds (0.75f);
		platform_move = true;
	}

	IEnumerator PlatformBackDelay()
	{
		yield return new WaitForSeconds (0.75f);
		platform_move = false;
	}
}
