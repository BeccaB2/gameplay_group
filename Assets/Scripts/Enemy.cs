using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Input
    public GameObject player;
    public Rigidbody rb;
    public Transform firePoint;
    public Animator anim;

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
    private static bool dead;
    private bool retreat;
    public static bool canMove;
    private bool dizzy;

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
    public int health = 3;



    // Use this for initialization
    void Start()
    {
        currentPoint = points[pointSelection];
        anim.SetBool("walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(canMove);
        if(characterControls.dead)
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
        else if (anim.GetBool("idle") == true || anim.GetBool("attack_03") == true || anim.GetBool("damage") == true || anim.GetBool("dizzy") == true || anim.GetBool("run") == true && anim.GetBool("walk") == true)
        {
            anim.SetBool("idle", false);
            anim.SetBool("attack_03", false);
            anim.SetBool("damage", false);
            anim.SetBool("run", false);
            anim.SetBool("walk", false);
        }

        if (dead)
        {
            Destroy(gameObject, despawnTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canAttack = true;
            Debug.Log("can attack: " + canAttack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canAttack = false;
            Debug.Log("can attack: " + canAttack);
        }
    }

    void DetectAttack()
    {
        if (Input.GetButtonDown("Attack1") || Input.GetKeyDown(KeyCode.Joystick1Button2) && canAttack && !dead)
        {
            noOfHits += 1;
            
            Debug.Log(noOfHits);

            if (noOfHits == health-1)
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
            if(anim.GetBool("run") == true)
            {
                anim.SetBool("run", false);
            }
            else if(anim.GetBool("idle") == true)
            {
                anim.SetBool("idle", false);
            }
            if(canMove && !dizzy)
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
            else if(anim.GetBool("walk") == true)
            {
                anim.SetBool("walk", false);
            }
            if(canMove && !dizzy)
            {
                anim.SetBool("run", true);
            }
           
            
            patrolling = false;
            if(!dead)
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
                else if(!dizzy)
                {
                    attackCoolDownTime = attackCoolDownTimeMain;
                    if(anim.GetBool("damage") == false)
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
                if(!dizzy || !dead)
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
        if(!dizzy || !dead)
        {
           anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("attack_03", true);
        
            yield return new WaitForSeconds(1);
            characterControls.health -= attackDamage;
            anim.SetBool("attack_03", false);
            if(!dead && !dizzy)
            {
               anim.SetBool("idle", true);
            }
        }
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
        //yield return new WaitForSeconds();
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
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("run", false);
        anim.SetBool("walk", false);
        anim.SetBool("idle", false);
        anim.SetBool("attack_03", false);
        anim.SetBool("damage", true);
        
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("damage", false);
        if(!dead)
        {
            anim.SetBool("idle", true);
        }
        
        canAttack = true;
    }
}
