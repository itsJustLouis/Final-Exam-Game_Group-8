using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies; // List of all enemies in the scene
    public GameObject[] spawnObjects; // Objects to spawn when an enemy dies
    private GameObject lastSpawned; // The last object that was spawned
    public int movingCount = 0;
    private void Update()
    {
        
        Debug.Log("ISMoving COunt"+movingCount);
        foreach (GameObject enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            if (enemyHealth.isMoving)
            {
                movingCount++;
            }

            if (movingCount > 2)
            {
                enemyHealth.StopMoving();
            }
            else
            {
                if (movingCount<2)
                {
                    enemyHealth.ResumeMoving();
                }
            }
        }
    }

    public void EnemyDied(GameObject enemy)
    {
        // Remove the enemy from the list
        enemies.Remove(enemy);

        // Spawn a new object at the enemy's position
        GameObject toSpawn = ChooseSpawnObject();
        Instantiate(toSpawn, enemy.transform.position, Quaternion.identity);
    }

    private GameObject ChooseSpawnObject()
    {
        GameObject chosen;

        do
        {
            chosen = spawnObjects[Random.Range(0, spawnObjects.Length)];
        } while (chosen == lastSpawned);

        lastSpawned = chosen;

        return chosen;
    }
}
