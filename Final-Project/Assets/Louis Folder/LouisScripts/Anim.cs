using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private Animator myAnim;
    private EnemyIntelligence myEnemyAIScript;
    private Damagable EnemyhealthScript;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myEnemyAIScript = GetComponent<EnemyIntelligence>();
        EnemyhealthScript = GetComponent<Damagable>();
        myAnim.SetBool("isMoving", false);
        myAnim.SetBool("Blow", false);
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
        if (myEnemyAIScript.followEnabled == false)
        {
            myAnim.SetBool("isMoving", false);
        }
        if (myEnemyAIScript.withinStoppingDistance == true)
        {
            myAnim.SetBool("isMoving", false);
        }
        if (EnemyhealthScript.Health <= 0)
        {
            myAnim.SetBool("isMoving", false);
            myAnim.SetBool("Blow", true);
        }
    }


}
