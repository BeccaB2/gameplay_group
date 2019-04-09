using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //Input
    public GameObject player;
    public CharacterController ccPlayer;
    public Transform firePoint;
    public Animator anim;
    public Image win;
    public Image lose;

    //uneditedable attributes
    int noOfHits;
    private int pointSelection;
    private Transform currentPoint;
    private bool patrolling = true;
    private bool canAttack;
    private float sphereRadius = 3f;
    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;
    private bool dead;
    private bool retreat;
    public static bool canMove;
    private bool dizzy;
    private bool gettingAttacked;


    //customisable attributes
    public Transform[] points;
    public float range = 20f;
    public float chaseRange = 15f;
    public float speed = 15f;
    public float chaseSpeed = 15f;
    public float attackCoolDownTime = 3f;
    public float attackCoolDownTimeMain;
    public float despawnTime = 10f;
    public int attackDamage = 5;
    public int health;
    public int maxHealth;
    public float thrust = 10f;
    float timer = 5;
    float timermain = 5;
    public bool trigger;


    // Use this for initialization
    void Start()
    {
        currentPoint = points[pointSelection];
        anim.SetBool("walk", true);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(noOfHits);

        if (dizzy && !dead)
        {
            if (timer > 0)
            {
                Debug.Log(timer);
                timer -= Time.deltaTime;
            }
            else
            {
                timer = timermain;
                dizzy = false;
                noOfHits = 0;
                anim.SetBool("dizzy", false);
                canMove = true;
            }
        }
        //Debug.Log(canMove);

        if (characterControls.dead)
        {
            anim.SetBool("idle", false);
            anim.SetBool("attack_03", false);
            anim.SetBool("damage", false);
            anim.SetBool("run", false);
            anim.SetBool("walk", false);
            anim.SetTrigger("dance");
        }
        else if (!dead || !dizzy)
        {
            Movement();
            DetectAttack();
        }

        if (dead && !trigger)
        {
            trigger = true;
            win.enabled = true;
            Destroy(gameObject, despawnTime);
        }
        else if (characterControls.health <= 0)
        {
            lose.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canAttack = true;
            Debug.Log("can attack: " + canAttack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canAttack = false;
            Debug.Log("can attack: " + canAttack);
        }
    }

    void DetectAttack()
    {
        if ((Input.GetButtonDown("Attack1") || Input.GetKeyDown(KeyCode.Joystick1Button2) && canAttack && !dead) && characterControls.isAttacking)
        {
            noOfHits += 1;

            Debug.Log(noOfHits);

            if (noOfHits == health - 1)
            {
                StartCoroutine(Dizzy());
            }
            else if (noOfHits >= health && !dead)
            {
                StartCoroutine(Die());
            }
            else
            {
                StartCoroutine(TakeDamage());
            }
        }
    }

    void Movement()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        var patrolDistance = Vector3.Distance(transform.position, currentPoint.transform.position);
        var playerDistance = Vector3.Distance(transform.position, player.transform.position);


        if (patrolling)
        {

            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * speed);
            if (anim.GetBool("run") == true)
            {
                anim.SetBool("run", false);
            }
            else if (anim.GetBool("idle") == true)
            {
                anim.SetBool("idle", false);
            }
            if (canMove && !dizzy)
            {
                anim.SetBool("walk", true);
            }

            if (transform.position == currentPoint.position)
            {
                retreat = false;
                pointSelection = Random.Range(0, points.Length);

                currentPoint = points[pointSelection];
            }
            transform.LookAt(points[pointSelection]);
        }


        if (distance <= range)/*&& !retreat)*/
        {
            if (anim.GetBool("idle") == true)
            {
                anim.SetBool("idle", false);
            }
            else if (anim.GetBool("walk") == true)
            {
                anim.SetBool("walk", false);
            }
            if (canMove && !dizzy)
            {
                anim.SetBool("run", true);
            }


            patrolling = false;
            if (!dead)
            {
                transform.LookAt(player.transform);
            }


            if (distance < 5)
            {
                anim.SetBool("run", false);

                if (attackCoolDownTime > 0)
                {
                    attackCoolDownTime -= Time.deltaTime * speed;
                }
                else if (!dizzy)
                {
                    attackCoolDownTime = attackCoolDownTimeMain;
                    if (anim.GetBool("run") == false)
                    {
                        StartCoroutine(Attack());
                    }
                }

                if (anim.GetBool("run") == true)
                {
                    anim.SetBool("run", false);
                }
                canMove = false;
            }
            else
            {
                if (!dizzy || !dead)
                {
                    canMove = true;
                }


                if (patrolDistance >= chaseRange)
                {
                    patrolling = true;
                    attackCoolDownTime = attackCoolDownTimeMain;
                    Debug.Log("patrol");
                    //retreat = true;
                }
            }
            if (canMove && !dizzy)
            {
                transform.position += transform.forward * chaseSpeed * Time.deltaTime;
            }
        }
    }


    IEnumerator Attack()
    {
        characterControls stats = player.GetComponent<characterControls>();
        if (!dizzy || !dead || !characterControls.isAttacking)
        {
            gettingAttacked = true;
            anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("attack_03", true);
            yield return new WaitForSeconds(1);
            var moveDirection = player.transform.position - transform.position;
            stats.getKnockedBack(moveDirection);
            characterControls.health -= attackDamage;
            anim.SetBool("attack_03", false);
            if (!dead && !dizzy)
            {
                anim.SetBool("idle", true);
            }
            //gettingAttacked = false;
        }
    }

    void Knockback()
    {
        var moveDirection = player.transform.position - transform.position;
        var startTime = Time.time;
        ccPlayer.SimpleMove(moveDirection * thrust);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("idle", false);
        anim.SetBool("run", false);
        anim.SetBool("walk", false);
        anim.SetBool("damage", false);
        anim.SetBool("attack_03", false);
        anim.SetBool("die", true);
        canMove = false;
        dead = true;
        
    }

    IEnumerator Dizzy()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("idle", false);
        anim.SetBool("run", false);
        anim.SetBool("walk", false);
        anim.SetBool("damage", false);
        anim.SetBool("attack_03", false);
        anim.SetBool("dizzy", true);
        dizzy = true;
        canMove = false;
    }

    IEnumerator TakeDamage()
    {
        if(characterControls.weaponCollected)
        {
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("run", false);
            anim.SetBool("walk", false);
            anim.SetBool("idle", false);
            anim.SetBool("attack_03", false);
            anim.SetBool("damage", true);

            canAttack = false;
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("damage", false);
            if (!dead)
            {
                anim.SetBool("idle", true);
            }

            canAttack = true;
        }

    }
}
