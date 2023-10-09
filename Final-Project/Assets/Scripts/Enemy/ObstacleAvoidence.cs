using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidence : Stearing
{
    [SerializeField]
    private float radius = 2f, agentColliderSize = 0.6f;


    [SerializeField]
    private bool showGizmo = true;
    //gizmo parameters 
    float[] dangerResultsTemp = null;


    public override (float[] danger, float[] interest) GetStearing(float[] danger, float[] interest, AIData aIData)
    {
        foreach (Collider2D obstacleCollider  in aIData.obstacles)
        {
            Vector2 directionToObstacle
                = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

            //calculate weight based on the distance Enemy<--->Obstacle
            float weight
                = distanceToObstacle <= agentColliderSize
                ? 1
                : (radius - distanceToObstacle) / radius;

            Vector2 directionToObstacleNormalised = directionToObstacle.normalized;

            //add obstacle parameters to the danger array
            for (int i  = 0; i < Directions.eightDirections.Count; i++)
            {
                float result = Vector2.Dot(directionToObstacleNormalised, Directions.eightDirections[i]);

                float valueToPutIn = result * weight;

                //override value only if it~is higher than the current one stored in the danger array
                if (valueToPutIn > danger[i])
                {
                    danger[i] = valueToPutIn;
                }
            }



        }

        dangerResultsTemp = danger;
        return (danger, interest);
    }


    private void OnDrawGizmos()
    {
        if (showGizmo ==false)
            return;
        if (Application.isPlaying && dangerResultsTemp != null)
        {
            if (dangerResultsTemp != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangerResultsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * dangerResultsTemp[i]);
                }
            }
        }
        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }

    public static class Directions
    {
        public static List<Vector2> eightDirections = new List<Vector2>{
            new Vector2(0,1).normalized,
            new Vector2(1,1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,1).normalized
        };
    }
}
