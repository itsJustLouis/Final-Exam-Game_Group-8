using Pathfinding; //this is from the unity package i downloaded, so basically i just imported it here
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyIntelligence : MonoBehaviour
{
    [Header("Pathfinding")] //this will show in the script editor
    public Transform target;   
    public float activateDistance = 10f; 
    public float pathUpdateSeconds = 0.5f; //this is how often we are going to update the A* algorithm that is used to detect colliders

    [Header("Physics")]
    public float speed = 2.5f;
    public float nextWaypointDistance = 3f; //this is how far away the enemy needs to be in order to start moving towards the next way point.


    [Header("Custom Behaviour")] 
    public bool followEnabled = false; //so if this is false, nothing in the script will do anything.
    public bool directionLookEnabled = true; //thats to see if the enemy will change direction or not.
    public bool withinStoppingDistance = false;

    [Header("Combat")]
    public float attackDistance = 0.5f; // The distance at which the enemy will initiate an attack

    [Header("Enemy Type")]
    public bool melee;
    public bool shooting;

    [Header("Melee")]
    public GameObject swordStrikePrefab;
    private GameObject swordStrikeInstance;
    private float lastAttackTime = 0.0f;
    private bool canAttack = true; 
    public float attackCooldown = 3.0f; 


    [Header("Shooting")]
    public GameObject fireThrowPrefab;
    private GameObject fireThrowInstance;

    private Path path; //path finding feature
    private int currentWayPoint = 0;
    Seeker seeker;
    Rigidbody2D rb;

    private Vector2 currentVelocity;

    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);

        if (TargetInDistance())
        {
            followEnabled = true;
        }
        else
        {
            followEnabled=false;
        }
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }



    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

      
        if (currentWayPoint >= path.vectorPath.Count)
        {
            return;
        }

        // Calculate direction
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed;

        // Movement using Vector2.SmoothDamp
        if (!withinStoppingDistance) 
        {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, force, ref currentVelocity, 0.5f);
        }

        // Next WayPoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }



      
        float targetDistance = Vector2.Distance(rb.position, target.transform.position);
        if (followEnabled && targetDistance < activateDistance)
        {
            if (targetDistance <= attackDistance)
            {
                // Stop the enemy movement
                rb.velocity = Vector2.zero;
                withinStoppingDistance = true;

                if (melee && canAttack)
                {
                    
                    swordStrikeInstance = Instantiate(swordStrikePrefab, transform.position, Quaternion.identity);
                    canAttack = false;
                    Destroy(swordStrikeInstance, 0.35f);
                }
                if (shooting && canAttack)
                {
                    fireThrowInstance = Instantiate(fireThrowPrefab, transform.position, Quaternion.identity);
                    canAttack = false;
                }
            }
            else
            {
                withinStoppingDistance = false;
            }
        }
        if (!canAttack && Time.time - lastAttackTime >= attackCooldown)
        {
            canAttack = true;
            lastAttackTime = Time.time;
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance; //checking if the enemy is within the activation distance 
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    private void Update()
    {
        if (TargetInDistance())
        {
            followEnabled = true;
        }
    }
}
