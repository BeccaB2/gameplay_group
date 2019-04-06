using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnSpline : MonoBehaviour
{
    public Transform[] waypointsList;

    public int currentWaypoint = 0;
    Transform targetWaypoint;

    public float speed;

    GameObject player;
    float characterScriptSpeed;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Mage");
        characterScriptSpeed = player.GetComponent<characterControls>().speed;
        waypointsList = GetComponentsInChildren<Transform>();
        //speed = characterScriptSpeed;
        speed = 30;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameObject.Find("Spline").GetComponent<EnterSpline>().onSpline == true)
        {
            if (currentWaypoint < waypointsList.Length)
            {
                if (targetWaypoint == null)
                {
                    targetWaypoint = waypointsList[currentWaypoint];
                }

                if (Input.GetKeyDown(KeyCode.W)/* controller?? */)
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, targetWaypoint.position, speed * Time.deltaTime);

                    //Vector3 targetRotate = new Vector3(targetWaypoint.rotation.x, player.transform.rotation.y, targetWaypoint.rotation.z);
                    //player.transform.LookAt(targetWaypoint.position,transform.up);
                }

                if (player.transform.position == targetWaypoint.position)
                {
                    currentWaypoint++;
                    targetWaypoint = waypointsList[currentWaypoint];
                }
            }
        }

    }
}
