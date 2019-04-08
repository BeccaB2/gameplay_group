using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float attackCoolDownTime;
    public float attackCoolDownTimeMain;
    public static bool isAttacking;
    public Text scoreText;
    public GameObject weapon;

    //CAMERA
    public Transform cam;
    Vector3 camF;
    Vector3 camR;

    //PHYSICS
    public float speed = 15;
    Vector3 intent;
    public static Vector3 velocity;
    Vector3 velocityXZ; 
    public float accel = 15;
    public float jumpVel = 40;
    float turnSpeed = 20;
    float turnSpeedLow = 22;
    float turnSpeedHigh = 30;
     bool jumped;

    //GRAVITY
    float grav = 20;
    bool grounded = false;

    // Collectables
    public static bool keyCollected = false;
    public static bool weaponCollected = false;
    public static bool doubleJumpActive = true;
    public static bool doubleSpeedActive = false;
    public static bool doubleSpeedPickedUp = false;
    public static bool doubleStrengthActive = false;
    public static int noOfEnemiesKilled = 0;
    public static int score = 0;

    // Use this for initialization
    void Start()
    {
        canInput = true;
        attackCoolDownTime = 0f;
    }

    void SetCountText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && !dead)
        {
            dead = true;
            velocity = new Vector3(0,0,0);
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

        if (doubleSpeedActive == true)
        {
            speed = 30;
        }
        else
        {
            speed = 15;
        }
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

    public void getKnockedBack(Vector3 direction)
    {
        characterControls.velocity = direction * 10;
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
            //Debug.Log("yes");

            if (EnterSpline.onSpline == false)
            {
                rot = Quaternion.LookRotation(intent);
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, rot, Time.deltaTime * turnSpeed);
            }

            //anim.SetBool("Running", true);
            if(!grounded)
            {
                anim.SetBool("Running", false);
            }
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
            if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button0) && !jumped))
            {
                Debug.Log("jump");
                velocity.y = jumpVel;
                anim.SetBool("Jump", true);
                anim.SetBool("Running", false);
                jumped = true;
            }
        }
        else if(Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button0)  && jumped)
        {
            if(doubleJumpActive == true)
            {
                 Debug.Log(doubleJumpActive);
                 Debug.Log("double jump");
                 anim.SetBool("DoubleJump", true);
                 velocity.y = jumpVel + 20;
            }
        }
    }

    void DoAttack()
    {
        Debug.Log(noOfEnemiesKilled);
            if (attackCoolDownTime > 0)
            {
                attackCoolDownTime -= Time.deltaTime;
            }
            else if (Input.GetButtonDown("Attack1") || Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                attackCoolDownTime = attackCoolDownTimeMain;
                StartCoroutine(Attack1());
            }
    }

    IEnumerator Attack1()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.01f);
        isAttacking = false;
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
            weapon.SetActive(true);
            Destroy(other.gameObject);
            weaponCollected = true;
        }

        if(other.gameObject.CompareTag("DSpeed"))
        {
            Destroy(other.gameObject);
            doubleSpeedPickedUp = true;

            // Timed
            StartCoroutine(SpeedRoutine());
        }

        if (other.gameObject.CompareTag("DStrength"))
        {
            Destroy(other.gameObject);
            doubleStrengthActive = true;
        
            // Timed
        }

        if (other.gameObject.CompareTag("Gem"))
        {
            //other.GetComponent<PickedUp>().picked_up = true;
            other.gameObject.SetActive(false);
            other.gameObject.tag = "DestroyedGem";
            score ++;
            SetCountText();
        }

    }

    IEnumerator SpeedRoutine()
    {
        doubleSpeedActive = true;
        yield return new WaitForSeconds(2);
        doubleSpeedActive = false;
    }
}



