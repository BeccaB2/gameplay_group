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
    private int pointSlection;
    private Transform currentPoint;
    private bool patrolling = true;
    private bool canAttack;
    private float sphereRadius = 3f;
    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;
    private static bool dead;


    //customisable attributes
    public Transform[] points;
    public float range = 20f;
    public float chaseRnage = 15f;
    public float speed = 15;
    public float chaseSpeed = 15;
    public float attackCoolDownTime = 3;
    public float attackCoolDownTimeMain;
    public int attackDamage = 5;
    public LayerMask layerMask;
    public int health = 3;



    // Use this for initialization
    void Start()
    {

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
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("idle", false);
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
        anim.SetBool("idle", true);
        canAttack = true;
    }
}
