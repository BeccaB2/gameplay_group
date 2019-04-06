using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectGem : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
       

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GemCollectionScript.gemCountS1 == 0)
        {
            Destroy(gameObject);
        }

    }
}
