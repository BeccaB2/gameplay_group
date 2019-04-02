using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUp : MonoBehaviour {

    public bool picked_up = false;
    static bool picked_up_static = false;
	// Use this for initialization
	void Start ()
    {
		//if(picked_up_static)
  //      {
  //          Destroy(this.gameObject);
  //      }
	}
    private void Update()
    {
        picked_up_static = picked_up;
    }
}
