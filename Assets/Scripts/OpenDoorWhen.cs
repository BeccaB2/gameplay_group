using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorWhen : MonoBehaviour {

    // Use this for initialization
    public GameObject door;

	// Update is called once per frame
	void Update ()
    {
		if(characterControls.noOfEnemiesKilled >= 2)
        {
                //this.transform.Translate(Vector3.right * Time.deltaTime * 10);
          door.SetActive(false);
            
        }
	}
}
