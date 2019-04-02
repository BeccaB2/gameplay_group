using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour {

    GameObject player;

    private Vector3 spawnPos;
    private Vector3 spawnRot;


    // Use this for initialization
    void Start ()
    {
        spawnPos = gameObject.transform.position;
        spawnRot = gameObject.transform.eulerAngles;

        player = GameObject.Find("PlayerBase");
        //player.transform.position = spawnPos;
        //player.transform.rotation = Quaternion.Euler(spawnRot);
        //Debug.Log(transform.position transform.rotation.eulerAngles);
        player.transform.SetPositionAndRotation(transform.position, transform.rotation);
        //player.transform.rotation = Quaternion.Euler(0,+90,0);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
