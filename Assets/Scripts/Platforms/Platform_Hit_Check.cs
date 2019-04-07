using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Hit_Check : MonoBehaviour {

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            GetComponentInParent<Platform_Lift1_Movement>().SetHitCheck(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            GetComponentInParent<Platform_Lift1_Movement>().SetHitCheck(false);
        }
    }
}
