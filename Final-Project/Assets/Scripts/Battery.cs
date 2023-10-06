using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public int batteryLevel = 5;
    public bool isRegenerating = false;
    public float regenerationDelay = 5f; // Time in seconds before regeneration starts
    public int currentAmount;
    public float regenerationTime = 5.0f; // Time in seconds to fully regenerate

    void Start()
    {
        currentAmount = batteryLevel;
    }

    void Update()
    {
        Debug.Log("Batter:"+ currentAmount);
        if (currentAmount < batteryLevel && !isRegenerating)
        {
           
            StartCoroutine(Regenerate());
        }
    }

    IEnumerator Regenerate()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(regenerationDelay);
        while (currentAmount < batteryLevel)
        {
            yield return new WaitForSeconds(regenerationTime / batteryLevel);
            currentAmount += 1;
          
        }
        isRegenerating = false;
    }


    public void DecreaseAmount()
    {
        if (currentAmount > 0)
        {
            currentAmount -= 1;
            Debug.Log(currentAmount);
        }
    }
}
