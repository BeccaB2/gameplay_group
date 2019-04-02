using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeecCollectable : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if (characterControls.doubleSpeedActive == true)
        {
            Destroy(gameObject);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
