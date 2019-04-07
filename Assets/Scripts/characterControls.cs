using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControls : MonoBehaviour
{
    //PLAYER
    public static float health = 100;

    //INPUT
    Vector2 input;
    public CharacterController controller;
    public Animator anim;
    public GameObject player;
    public bool canInput;
    public GameObject dustCloud;
    public GameObject runCloud;
    public static bool dead;

    //CAMERA
    public Transform cam;
    Vector3 camF;
    Vector3 camR;

    //PHYSICS
    public float speed = 15;
    Vector3 intent;
    Vector3 velocity;
    Vector3 velocityXZ;
    public float accel = 15;
    public float jumpVel = 20;
    float turnSpeed = 20;
    float turnSpeedLow = 22;
    float turnSpeedHigh = 30;
    bool jumped;

    //GRAVITY
    float grav = 10;
    bool grounded = false;

    // Collectables
    public static bool keyCollected = false;
    public static bool weaponCollected = false;
    public static bool doubleJumpActive = false;
    public static bool doubleSpeedActive = false;
    public static bool doubleStrengthActive = false;

    public static int score = 0;

    // Use this for initialization
    void Start()
    {
        canInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && !dead)
        {
            dead = true;
            anim.SetTrigger("dead");
        }

        ManageInput();
        CalculateCamera();
        CalculateGround();
        if(canInput && !dead)
        {
            DoMove();
            DoGravity();
            DoJump();
            DoAttack();
        }
        
        controller.Move(velocity * Time.deltaTime);

        //Debug.Log("Gems =" + score);
        //Debug.Log(health);

        //if (keyCollected == true)
        //{
        //    Debug.Log("Got it");
        //}
    }
    void ManageInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
    }

    void CalculateCamera()
    {
        camF = cam.forward;
        camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void CalculateGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position + Vector3.up * 0.1f, -Vector3.up, out hit, 0.2f))
        {
            grounded = true;
            Enemy.canMove = true;
            
            if (anim.GetBool("Jump") == true)
            {
                Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
                anim.SetBool("Jump", false);
            }
            
            if(anim.GetBool("DoubleJump") == true)
            {
                anim.SetBool("DoubleJump", false);
            }
            jumped = false;
        }
        else
        {
            grounded = false;
            Enemy.canMove = false;
            
        }
    }

    void DoMove()
    {
        //direction the camera is facing
        intent = camF * input.y + camR * input.x;

        float tS = velocity.magnitude / speed;
        turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, tS);

        if (input.magnitude > 0)
        {
            if (grounded)
            {
                Instantiate(runCloud, transform.position, runCloud.transform.rotation);
                anim.SetBool("Running", true);
            } 
            Quaternion rot = Quaternion.LookRotation(intent);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, rot, Time.deltaTime * turnSpeed);
        }
        else if(input.magnitude <= 0 && grounded)
        {

            anim.SetBool("Running", false);

        }


        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, player.transform.forward * input.magnitude * speed, accel * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);

    }

    void DoGravity()
    {
        if (grounded)
        {
            velocity.y = -0.5f;
        }
        else
        {
            velocity.y -= grav * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10, 10);
    }

    void DoJump()
    {
        if (grounded)
        {
            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button0) && !jumped)
            {
                velocity.y = jumpVel;
                anim.SetBool("Jump", true);
                anim.SetBool("Running", false);
                jumped = true;
            }
        }
        else if(Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button0) && doubleJumpActive && jumped)
        {
            anim.SetBool("DoubleJump", true);
            velocity.y = jumpVel + 20;
        }
    }

    void DoAttack()
    {
        if(Input.GetButtonDown("Attack1") || Input.GetKey(KeyCode.Joystick1Button2))
        {
            StartCoroutine(Attack1());
        }
    }

    IEnumerator Attack1()
    {
        anim.SetBool("Attack1", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Attack1", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            // Checks if player hits collectable & deletes it
            Destroy(other.gameObject);
            keyCollected = true;
        }

        if (other.gameObject.CompareTag("DJump"))
        {
            // Checks if player hits collectable & deletes it
            Destroy(other.gameObject);
            doubleJumpActive = true;
        }

        if(other.gameObject.CompareTag("Weapon"))
        {
            Destroy(other.gameObject);
            weaponCollected = true;
        }

        if(other.gameObject.CompareTag("DSpeed"))
        {
            Destroy(other.gameObject);
            doubleSpeedActive = true;
            
            // Timed?
        }

        if (other.gameObject.CompareTag("DStrength"))
        {
            Destroy(other.gameObject);
            doubleStrengthActive = true;

            // Timed?
        }

        if (other.gameObject.CompareTag("Gem"))
        {
            //other.GetComponent<PickedUp>().picked_up = true;
            other.gameObject.SetActive(false);
            GemCollectionScript.gemCountS1--;
            score ++;
        }

    }
}



