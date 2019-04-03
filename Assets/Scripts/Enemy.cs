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
        DetectAttack();
        Movement();
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

            if (noOfHits >= health)
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

        if (patrolling)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * speed);
            anim.SetBool("walk", true);

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
            anim.SetBool("run", true);

            if (distance < 5)
            {
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
                if (attackCoolDownTime > 0)
                {
                    attackCoolDownTime -= Time.deltaTime * speed;
                }
                else
                {
                    attackCoolDownTime = attackCoolDownTimeMain;
                    Attack();
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
                    anim.SetBool("idle", false);
                    anim.SetBool("run", false);
                    anim.SetBool("walk", true);
                    Debug.Log("patrol");
                    retreat = true;
                }
            }

            if (canMove)
            {
                transform.position += transform.forward * chaseSpeed * Time.deltaTime;
                anim.SetBool("walk", false);
                anim.SetBool("idle", false);
                anim.SetBool("run", true);
            }


        }

    }

    void Attack()
    {
        characterControls.health -= attackDamage;
        anim.SetBool()

    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("idle", false);
        anim.SetBool("run", false);
        anim.SetBool("walk", false);
        anim.SetBool("die", true);
        dead = true;
    }

    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("damage", true);
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("damage", false);
        anim.SetBool("run", false);
        anim.SetBool("idle", true);
        canAttack = true;
    }
}
