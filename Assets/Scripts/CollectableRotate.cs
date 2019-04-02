using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableRotate : MonoBehaviour
{
    public float speed = 0.5f;

    // Rotation values
    public float xRot;
    public float yRot;
    public float zRot;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(xRot, yRot, zRot) * (speed * Time.deltaTime));
    }
}
