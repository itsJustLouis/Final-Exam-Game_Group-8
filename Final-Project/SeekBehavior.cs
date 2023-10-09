using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehavior : Stearing
{
    [SerializeField]
    private float targetRechedThreshold = 0.5f;

    [SerializeField]
    private bool showGizmo = true;

    bool reachedLastTarget = true;

    //gizmo parameters
    private Vector2 targetPositionsCached;
    private float[] interestsTemp;

    public override (float[] danger, float[] interest) GetStearing(float[] danger, float[] interest, AIData aIData)
    {
       
        //if we dont have a target stop seeking
        //else set a new target
        if (reachedLastTarget)
        {
            if (aIData.targets == null || aIData.targets.Count <=0)
            {
                aIData.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                reachedLastTarget = false;
                aIData.currentTarget = aIData.targets.OrderBy
                    (target => Vector2.Distance(target.position, transform.position)).First();
            }
        }


        //cach the last position only if we still see the target (if the target collection is not empty
        if (aIData.currentTarget != null && aIData.targets != null && aIData.targets.Contains(aIData.currentTarget))
        targetPositionsCached = aIData.currentTarget.position;

        //first check if we have reached the target
        if (Vector2.Distance(transform.position, targetPositionsCached)< targetRechedThreshold)
        {
            reachedLastTarget = true;
            aIData.currentTarget = null;
            return(danger, interest);
        }
        //if we havent yet reached the target do the main logic of finding the interest directions
        Vector2 directionToTarget = (targetPositionsCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            //accept only directions at the less than 90 degress to the target direction
            if (result >0)
            {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }
        interestsTemp = interest;
        return (danger, interestsTemp);

    }

    private void OnDrawGizmos()
    {
        if (showGizmo == false)
            return;
        Gizmos.DrawSphere(targetPositionsCached, 0.2f);


        if (Application.isPlaying && interestsTemp != null)
        {
            if (interestsTemp != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < interestsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestsTemp[i]);
                }
                if (reachedLastTarget == false)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targetPositionsCached, 0.1f);
                }
            }
        }


    }




    public static class Directions
    {
        public static List<Vector2> eightDirections = new List<Vector2>()
        {
            new Vector2(0,1).normalized,
            new Vector2(1,1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,1).normalized,
        };
    }
}
