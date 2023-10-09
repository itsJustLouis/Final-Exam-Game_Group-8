using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<Stearing> stearingBehavior;

    [SerializeField]
    private AIData aIData;

    [SerializeField]
    private List<Detector> detectors;

    
   
    //delaye enemy attack here and enemy tracking
    [SerializeField]
    private float detectionDelay = 0.0f, aiUpdateDelay = 0.06f, attackDelay = 1f;

    [SerializeField]
    private float attackDistance = 0.5f;


    //iputs sent from enemy ai to the enemy controller
    public UnityEvent onAttackPressed;
    public UnityEvent<Vector2> OnMovement, OnpointerInput;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextSolver movementDirectionSolver;

    bool following = false;
    private void Start()
    {
        //detecting player and obstacle around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }


    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aIData);
        }
      



    }

    private void Update()
    {
        //enemy ai movement based on target availability
        if (aIData.currentTarget != null)
        {
            //look for target
            OnpointerInput?.Invoke(aIData.currentTarget.position);
            if (following == false)
            {
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aIData.GetTargetsCount() >0)
        {
            //target acquisition logic
            aIData.currentTarget = aIData.targets[0];
        }


        //moving the Agent
        OnpointerInput?.Invoke(movementInput);

    }


    private IEnumerator ChaseAndAttack()
    {
        if (aIData.currentTarget == null)
        {
            //stopping Logic
            Debug.Log("stopping");
            movementInput = Vector2.zero;
            following = false;
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aIData.currentTarget.position, transform.position);

            if (distance < attackDistance)
            {
                //attack Logic
                movementInput = Vector2.zero;
                onAttackPressed?.Invoke();
                yield return new WaitForSeconds(attackDelay);

                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                //chase Logic
                movementInput = movementDirectionSolver.GetDirectionToMove(stearingBehavior, aIData);
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            }
        }
    }

}
