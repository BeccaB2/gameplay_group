
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothingCamera : MonoBehaviour
{

    public float CameraMoveSpeed = 120.0f;
    public GameObject FollowTarget;
    Vector3 follow_pos;
    public float fixedAngle = 200.0f;
    public float inputSensitivity = 100.0f;
    public GameObject Camera;
    public GameObject Player;
    private float xDistanceToPlayer;
    private float yDistanceToPlayer;
    private float zDistanceToPlayer;
    private float mouseX;
    private float mouseY;
    private float finalInputX;
    private float finalInputZ;
    private float smoothX;
    private float smootY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;


    // Use this for initialization
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = mouseX;
        finalInputZ = mouseY;

        rotX += finalInputZ * inputSensitivity * Time.deltaTime;
        rotY += finalInputX * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -fixedAngle, fixedAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;


    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = FollowTarget.transform;
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
