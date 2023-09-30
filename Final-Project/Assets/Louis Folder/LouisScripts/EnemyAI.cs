using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; //this is from the unity package i downloaded, so basically i just imported it here

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")] //this will show in the script editor
    public Transform target;   //the target the enemy is targeting
    public float activateDistance = 50f; //this will be the activation distance
    public float pathUpdateSeconds = 0.5f; //this is how often we are going to update the A* algorithm that is used to detect colliders

    [Header("Physics")]
    public float speed = 150f;
    public float nextWaypointDistance = 3f; //this is how far away the enemy needs to be in order to start moving towards the next way point.
   

    [Header("Custom Behaviour")] //this is useful for making enemies dumb.
    public bool followEnabled = true; //so if this is false, nothing in the script will do anything.
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
    private float lastAttackTime = 0.0f; // Track the time of the last attack.
    private bool canAttack = true; // Flag to track if the enemy can attack.
    private float attackCooldown = 3.0f; // Cooldown time between attacks.


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
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
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

            // Reached the end of the path
            if (currentWayPoint >= path.vectorPath.Count)
            {
                return;
            }

            // Calculate direction
            Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
            Vector2 force = direction * speed;

            // Movement using Vector2.SmoothDamp
            if (!withinStoppingDistance) // apply SmoothDamp only when the enemy is not within the stopping distance
            {
                rb.velocity = Vector2.SmoothDamp(rb.velocity, force, ref currentVelocity, 0.5f);
            }

            // Next WayPoint
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
            if (distance < nextWaypointDistance)
            {
                currentWayPoint++;
            }

            // Direction sprite flip, might cancel this though

            // Attack behavior
            float targetDistance = Vector2.Distance(rb.position, target.transform.position);
            if (followEnabled && targetDistance < activateDistance)
            {
            if (targetDistance <= attackDistance)
            {
                // Stop the enemy's movement
                rb.velocity = Vector2.zero;
                withinStoppingDistance = true;
                if (melee && canAttack)
                {
                    // Instantiate the sword strike animation.
                    swordStrikeInstance = Instantiate(swordStrikePrefab, transform.position, Quaternion.identity);

                    // Set the flag to indicate the enemy has attacked.
                    canAttack = false;
                    Destroy(swordStrikeInstance, 0.25f);
                }
                if(shooting && canAttack)
                {
                    // Instantiate the sword strike animation.
                    fireThrowInstance = Instantiate(fireThrowPrefab, transform.position, Quaternion.identity);

                    // Set the flag to indicate the enemy has attacked.
                    canAttack = false;
                    Destroy(fireThrowInstance, 0.5f);
                }
                // Add your attack code here, dealing damage to the target

            }
            else
            {
                // Resume movement when outside stopping distance
                withinStoppingDistance = false;
            }
        }

        // Check if the cooldown period has passed and reset the canAttack flag.
        if (!canAttack && Time.time - lastAttackTime >= attackCooldown)
        {
            canAttack = true;
            lastAttackTime = Time.time;
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance; //checking if the enemy is within the activation distance or scope of the target
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
}
