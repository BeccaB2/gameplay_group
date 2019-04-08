using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnSpline : MonoBehaviour
{
    public Transform[] waypointsList;

    public int currentWaypoint = 0;
    public Transform targetWaypoint;

    public float speed;

    public GameObject player;
    float characterScriptSpeed;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Mage");
        //characterScriptSpeed = player.GetComponent<characterControls>().speed;
        //waypointsList = GetComponentsInChildren<Transform>();
        
        // speed = characterScriptSpeed;
    }

    // Update is called once per frame
    void Update ()
    {
        if (characterControls.doubleSpeedActive == true)
        {
            speed = player.GetComponent<characterControls>().speed;
        }
        else
        {
            speed = 15;
        }

        if (EnterSpline.onSpline == true)
        {

            if (currentWaypoint < this.waypointsList.Length)
            {
                if (targetWaypoint == null)
                {
                    targetWaypoint = waypointsList[currentWaypoint];
                }

                if (Input.GetKey(KeyCode.W)/* controller?? */)
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, targetWaypoint.position, speed * Time.deltaTime);

                    Vector3 targetRotate = new Vector3(targetWaypoint.position.x, player.transform.position.y, targetWaypoint.position.z);
                    player.transform.LookAt(targetRotate);
                }

                if (player.transform.position == targetWaypoint.position)
                {
                    currentWaypoint++;
                    targetWaypoint = waypointsList[currentWaypoint];
                }
            }
         
            // Check if respawned to reset spline?
        }

    }

 }
