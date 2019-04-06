using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSpline : MonoBehaviour {

    public Camera normalCam;
    public Camera sideCam;

    public bool onSpline = false;

    // Use this for initialization
    void Start ()
    {
        normalCam.enabled = true;
        sideCam.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            normalCam.enabled = false;
            sideCam.enabled = true;
            onSpline = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            normalCam.enabled = true;
            sideCam.enabled = false;
            onSpline = false;
        }

    }
}
