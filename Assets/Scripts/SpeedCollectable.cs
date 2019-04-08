using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollectable : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if (characterControls.doubleSpeedPickedUp == true)
        {
            Destroy(gameObject);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
