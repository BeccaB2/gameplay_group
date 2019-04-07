using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

    public float minDistance = 2.0f;
    public float maxDistance = 15.0f;
    public float smooth = 10.0f;
    Vector3 dollyDirection;
    public Vector3 dollyDirAdjusted;
    public float distance;

    // Use this for initialization
    void Awake()
    {
        dollyDirection = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCamPos = transform.parent.TransformPoint(dollyDirection * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCamPos, out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDirection * distance, Time.deltaTime * smooth);
    }
}
