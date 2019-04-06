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
    private bool canMove;

    //customisable attributes
    public Transform[] points;
    public float range = 20f;
    public float chaseRange = 15f;
    public float speed = 15f;
    public float chaseSpeed = 15f;
    public float attackCoolDownTime = 3f;
    public float attackCoolDownTimeMain;
    public int attackDamage = 5;
    public int health = 3;



    // Use this for initialization
    void Start()
    {
        currentPoint = points[pointSelection];
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!dead)
        {
            Movement();
            DetectAttack();
        }
        else
        {
            if(anim.GetBool("idle") == true ||anim.GetBool("attack_03") == true || anim.GetBool("damage") == true)
            {
                anim.SetBool("idle", false);
                anim.SetBool("attack_03", false);
                anim.SetBool("damage", false);
            }
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
        if (Input.GetButtonDown("Attack1") && canAttack && !dead)
        {
            noOfHits += 1;
            
            Debug.Log(noOfHits);

            if (noOfHits >= health && !dead)
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
        var patrolDistance = Vector3.Distance(transform.position, currentPoint.position);
        var playerDistance = Vector3.Distance(transform.position, player.transform.position);

        //Debug.Log(distance);
        //Debug.Log(patrolDistance);
        //Debug.Log(playerDistance);

        if (patrolling)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * speed);
    

            if (transform.position == currentPoint.position)
            {
                retreat = false;
                pointSelection = Random.Range(0, points.Length);

                currentPoint = points[pointSelection];
            }
            transform.LookAt(points[pointSelection]);
        }


        if (distance <= range && !retreat)
        {
            patrolling = false;
            transform.LookAt(player.transform);

            if (distance < 5)
            {

                if (attackCoolDownTime > 0)
                {
                    attackCoolDownTime -= Time.deltaTime * speed;
                }
                else
                {
                    attackCoolDownTime = attackCoolDownTimeMain;

                    if(anim.GetBool("dizzy") != true && !dead)
                    {
                         StartCoroutine(Attack());
                    }
                    
                }
                canMove = false;
            }
            else
            {
                canMove = true;

                if (patrolDistance >= chaseRange)
                {
                    patrolling = true;
                    attackCoolDownTime = attackCoolDownTimeMain;
                    Debug.Log("patrol");
                    retreat = true;
                }
            }
            if (canMove)
            {
                transform.position += transform.forward * chaseSpeed * Time.deltaTime;
            }
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("idle", false);
        anim.SetBool("walk", false);
        anim.SetBool("run", false);
        anim.SetBool("attack_03", true);
        
        yield return new WaitForSeconds(1);
        characterControls.health -= attackDamage;
        anim.SetBool("attack_03", false);
        if(!dead)
        {
           anim.SetBool("idle", true);
        }
        
        Debug.Log(characterControls.health);

    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("idle", false);
        //anim.SetBool("run", false);
        //anim.SetBool("walk", false);
        anim.SetBool("damage", false);
        anim.SetBool("attack_03", false);
        anim.SetBool("die", true);
        //yield return new WaitForSeconds();
        dead = true;
    }

    IEnumerator Dizzy()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("idle", false);
        //anim.SetBool("run", false);
        //anim.SetBool("walk", false);
        anim.SetBool("damage", false);
        anim.SetBool("attack_03", false);
        anim.SetBool("dizzy", true);


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
