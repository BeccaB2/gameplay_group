using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCam_Controls : MonoBehaviour
{

    public GameObject main_cam;
    public GameObject door_cam;

    public GameObject start_point;
    public GameObject target_point;
    public GameObject button_point;
    public GameObject door_point;

    Transform target_point1;
    Transform target_point2;
    Transform button_target;
    Transform door_target;

    int stage = 0;
    bool active = false;
    bool disable = false;

    // Use this for initialization
    void Start()
    {
        target_point1 = start_point.transform;
        target_point2 = target_point.transform;
        button_target = button_point.transform;
        door_target = door_point.transform;

        transform.position = target_point1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (stage == 0)
            {
                camStage0();
            }
            if (stage == 1)
            {
                camStage1();
            }
            if (stage == 2)
            {
                if (!disable)
                {
                    disable = true;
                    StartCoroutine("SwitchDelay");
                }
            }
        }
    }

    private void camStage0()
    {
        button_target.position = Vector3.MoveTowards(button_target.position, door_target.position, 10f * Time.deltaTime);
        door_cam.transform.LookAt(button_target.position);

        if (button_target.position == door_target.position)
        {
            stage = 1;
        }
    }
    private void camStage1()
    {
        transform.position = Vector3.MoveTowards(transform.position, target_point2.transform.position, 10f * Time.deltaTime);

        if (transform.position == target_point2.transform.position)
        {
            stage = 2;
        }
    }

    public void setDoorCamActive()
    {
        active = true;
    }

    IEnumerator SwitchDelay()
    {
        yield return new WaitForSeconds(0.25f);
        active = false;
        main_cam.SetActive(true);
        door_cam.SetActive(false);
    }
}
