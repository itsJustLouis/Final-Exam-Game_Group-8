using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLight : MonoBehaviour
{
    public Transform enemyTransform; // Reference to the enemy's Transform
    public float torchOffsetDistance = 1.0f; // Adjust this value to control the torch offset

    private void Update()
    {
        if (enemyTransform != null)
        {
            // Calculate the position offset based on the enemy's forward direction
            Vector3 offset = enemyTransform.up * torchOffsetDistance;

            // Set the torch's position to the calculated position offset
            transform.position = enemyTransform.position + offset;

            // Set the torch's rotation to match the enemy's rotation
            transform.rotation = enemyTransform.rotation;
        }
    }
}
