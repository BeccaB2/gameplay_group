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
            gameObject.SetActive(false);
        }
        else if (characterControls.doubleSpeedPickedUp == false)
        {
            gameObject.SetActive(true);
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (characterControls.doubleSpeedPickedUp == true)
        {
            gameObject.SetActive(false);
        }
        else if(characterControls.doubleSpeedPickedUp == false)
        {
            gameObject.SetActive(true);
        }

    }
}
