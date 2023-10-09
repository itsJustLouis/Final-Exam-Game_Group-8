using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Detector
{
    [SerializeField]
    private float targetDetectionRange = 2;

    [SerializeField]
    private LayerMask obstacleLayerMask, PlayerLayerMask;

    //gizmo parameters
    private List<Transform> colliders;

    [SerializeField]
    private bool showGizmos = false;
    public override void Detect(AIData aIData)
    {
        
         
        //find out if player is near
        Collider2D playerCollider =Physics2D.OverlapCircle(transform.position, targetDetectionRange, PlayerLayerMask);

        if (playerCollider != null)
        {
            //check if you see the player
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstacleLayerMask);

            //make sure that the collider we see is on the "Player" layer
            if (hit.collider != null && (PlayerLayerMask & (1 << hit.collider.gameObject.layer)) !=0)
            {
                Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                colliders = new List<Transform>() { playerCollider.transform };

            }else
            {
                colliders = null;
            }


        }else
        {
            //enemy doesnt see the player
            colliders = null;
        }
        aIData.targets = colliders;

    }


    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false)
            return;
        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

        if (colliders == null)
            return;
        Gizmos.color = Color.magenta;
        foreach (var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }

}
