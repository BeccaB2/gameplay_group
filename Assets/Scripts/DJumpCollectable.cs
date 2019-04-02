using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJumpCollectable : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if (characterControls.doubleJumpActive == true)
        {
            Destroy(gameObject);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
