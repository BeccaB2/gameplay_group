using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSpline : MonoBehaviour {

    public Camera normalCam;
    public Camera sideCam;

    public static bool onSpline = false;

    // Use this for initialization
    void Start ()
    {
        //normalCam.enabled = true;
        //sideCam.enabled = false;
        //onSpline = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            normalCam.gameObject.SetActive(false);
            sideCam.gameObject.SetActive(true);
            onSpline = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            normalCam.gameObject.SetActive(true);
            sideCam.gameObject.SetActive(false);
            onSpline = false;
        }

    }
}
