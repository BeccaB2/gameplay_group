using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectable : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		if(characterControls.keyCollected == true)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
