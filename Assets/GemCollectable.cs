using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollectable : MonoBehaviour
{
    private PickedUp gemsCollected;
    // Use this for initialization
    void Start ()
    {
        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
        foreach (GameObject oneGem in gems)
        {
            gemsCollected = oneGem.GetComponent<PickedUp>();

            if (gemsCollected.picked_up == true)
            {
                Destroy(gameObject);
            }

        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
