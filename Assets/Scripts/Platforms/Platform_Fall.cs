using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Fall : MonoBehaviour {

    private GameObject player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = this.transform;
            StartCoroutine("PlatformFallDelay");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }
    }

    IEnumerator PlatformFallDelay()
    {
        yield return new WaitForSeconds(2.0f);
        GetComponent<Rigidbody>().useGravity = true;

    }
}
