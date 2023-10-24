using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLight : MonoBehaviour
{
    public Transform enemyTransform; 
    public float torchOffsetDistance = 1.0f; 

    private void Update()
    {
        if (enemyTransform != null)
        {
  
            Vector3 offset = enemyTransform.up * torchOffsetDistance;

            // Set the torch position
            transform.position = enemyTransform.position + offset;

            // Set the torch rotation
            transform.rotation = enemyTransform.rotation;
        }
    }
}
