using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Multipoint_HitCheck : MonoBehaviour
{
    public GameObject stepping_point;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            GetComponentInParent<Platform_Moving1_Movement>().SetHitCheck(true);
            stepping_point.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            GetComponentInParent<Platform_Moving1_Movement>().SetHitCheck(false);
            stepping_point.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
