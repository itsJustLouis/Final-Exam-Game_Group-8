using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;
    private int stoppingPoint;
    // Walk speed
    [SerializeField]
    private float moveSpeed = 2f;

    // Index of current waypoint from which the enemy walks from to the next one
    private int waypointIndex = 0;
    private Animator myAnim;
    // Time to stop for an attack
    private float attackStopTime = 4f;

    private bool isAttacking = false; 
    private bool hasAttacked = false;

    public string playerTag = "Player";
    public float detectionRange = 5f; // The range within which the enemy can detect the player
    public float detectionInterval = 1f; // How often to check for the player
    private float posX;
    private bool isPlayerDetected = false;

    private Coroutine detectionCoroutine;

    [Header("Swarming Enemies")]
    public GameObject EnemyPrefab;
    private GameObject enemyInstance;
    public List<GameObject> aliveEnemies = new List<GameObject>();

    public int maxInstances = 5; // Set the maximum number of instances you want to create
    private int currentInstanceCount = 0;
    private void Start()
    {
        myAnim = GetComponent<Animator>();
        // Set position of enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        posX = Random.Range(2, 5);
        detectionCoroutine = StartCoroutine(DetectPlayer());

            if (isPlayerDetected)
            {
            CheckEnemiesStatus();
            // player detection
            Debug.Log("Player detected!");
            StartCoroutine(SwarmPlayer());

            if (currentInstanceCount < maxInstances)
            {
                Vector3 swarmSpawnPosition = transform.position + new Vector3(posX, 0f, 0f);
                enemyInstance = Instantiate(EnemyPrefab, swarmSpawnPosition, Quaternion.identity);
                aliveEnemies.Add(enemyInstance); // Add the new enemy to the list
                currentInstanceCount++;
            }
        }

    
        // Check if the enemy is attacking
        if (isAttacking)
        {
            // Check if the enemy has attacked already
            if (!hasAttacked)
            {
                hasAttacked = true;
                stoppingPoint = Random.Range(0, waypoints.Length);
                Debug.Log(stoppingPoint);
            }
            return;
        }
        Move();
    }

    private void Move()
    {
       
        if (waypointIndex < waypoints.Length)
        {
            // Move enemy from current waypoint to the next
            myAnim.SetBool("isMoving", true);
            myAnim.SetFloat("moveX", (waypoints[waypointIndex].transform.position.x - transform.position.x));
            myAnim.SetFloat("moveY", (waypoints[waypointIndex].transform.position.y - transform.position.y));

            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            
            // If enemy reaches position of waypoint he walked towards, then waypointIndex is increased by 1 and the enemy starts moving to the next one
            if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.01f)
            {
                if (waypointIndex == stoppingPoint)
                 {
                    // Stop the enemy and initiate an attack
                    myAnim.SetBool("isMoving", false);
                    isAttacking = true;
                    StartCoroutine(AttackAndResume());
                }
                else
                {
                    // Move to the next waypoint
                    waypointIndex++;
                }
            }
        }
        else
        {
            // If the enemy reached the last waypoint, reset the index to 0 for looping
            waypointIndex = 0;
        }
    }


    private IEnumerator AttackAndResume()
    {

        // Stop the enemy's movement during the attack
        moveSpeed = 0f;

        // Wait for the attack stop time
        yield return new WaitForSeconds(attackStopTime);

        // continue movement after attack
        moveSpeed = 2f;

        // Move to the next waypoint after attack
        waypointIndex++;

        // Reset the attack flag
        isAttacking = false;
        hasAttacked = false;
    }

    private IEnumerator DetectPlayer()
    {
      while (true)
     {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(playerTag))
            {
                // Player detected
                isPlayerDetected = true;
                yield break; 
            }
        }

        // Player not detected
        isPlayerDetected = false;
        yield return new WaitForSeconds(detectionInterval);
     }
    }

    private IEnumerator SwarmPlayer()
    {
        CheckEnemiesStatus();
        yield return new WaitForSeconds(1f);
        if (currentInstanceCount < maxInstances)
        {
            Vector3 swarmSpawnPosition = transform.position + new Vector3(posX, 0f, 0f);
            enemyInstance = Instantiate(EnemyPrefab, swarmSpawnPosition, Quaternion.identity);
            aliveEnemies.Add(enemyInstance); // Add the new enemy to the list
            currentInstanceCount++;
        }

    }
    private void CheckEnemiesStatus()
    {
        for (int i = aliveEnemies.Count - 1; i >= 0; i--)
        {
            if (aliveEnemies[i] == null)
            {
                //enemy destroyed
                aliveEnemies.RemoveAt(i);
                currentInstanceCount--;
            }
        }

        // If all enemies destroyed, reset count
        if (aliveEnemies.Count == 0)
        {
            currentInstanceCount = 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.6f, 0.3f, 0.3f, 0.35f);
        Gizmos.DrawSphere(transform.position, detectionRange);
    }
}


