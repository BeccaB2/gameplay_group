using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthCollectable : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if (characterControls.doubleStrengthActive == true)
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
