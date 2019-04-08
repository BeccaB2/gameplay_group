using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killEnemy : MonoBehaviour {

    // Use this for initialization

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            characterControls.health = 0;
        }
    }
}
