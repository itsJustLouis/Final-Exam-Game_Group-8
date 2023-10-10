using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private Animator myAnim;
    private EnemyAI myEnemyAIScript;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myEnemyAIScript = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        FollowPlayer();
    }
    public void FollowPlayer()
    {
        if (myEnemyAIScript.followEnabled == true)
        {
            myAnim.SetBool("isMoving", true);
            myAnim.SetFloat("moveX", (myEnemyAIScript.target.position.x - transform.position.x));
            myAnim.SetFloat("moveY", (myEnemyAIScript.target.position.y - transform.position.y));
        }
        if (myEnemyAIScript.withinStoppingDistance == true)
        {
            myAnim.SetBool("isMoving", false);
        }
    }
}
