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
        
        player.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
