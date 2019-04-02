using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFloat : MonoBehaviour
{
    // Finds position of the object
    private Vector3 startPos;
    float yPos;

    // Checks the direction of travel
    public bool movingUp = true;

    // Speed & amount of movement allowed
    public float speed = 2f;
    public float moveDistance = 1.5f;

    // Use this for initialization
    void Start ()
    {
        startPos = transform.position;
        yPos = transform.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var tempPos = transform.position;

        if(movingUp == true)
        {
            tempPos.y += 1 * Time.deltaTime * speed;
            transform.position = tempPos;

            if(transform.position.y >= yPos + moveDistance)
            {
                movingUp = false;
            }
        }

        if(movingUp == false)
        {
            tempPos.y -= 1 * Time.deltaTime * speed;
            transform.position = tempPos;

            if(transform.position.y <= yPos - moveDistance)
            {
                movingUp = true;
            }
        }
	}
}
