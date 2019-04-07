using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectGem : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (gameObject.tag == "DestroyedGem")
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}
