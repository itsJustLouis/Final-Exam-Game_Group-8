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
    private GameObject[] batteryUIObjects;

    void Start()
    {
        currentAmount = batteryLevel;
        batteryUIObjects = GameObject.FindGameObjectsWithTag("BatteryUI");
        for (int i = 0; i < batteryUIObjects.Length; i++)
        {
            if (i >= currentAmount)
            {
                batteryUIObjects[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        Debug.Log("Battery:" + currentAmount);
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
            if (currentAmount < batteryLevel)
            {
                currentAmount += 1;
                batteryUIObjects[currentAmount - 1].SetActive(true);
                Debug.Log("Battery level after regeneration: " + currentAmount);
            }
            else
            {
                break;
            }
        }
        isRegenerating = false;
    }

    public void DecreaseAmount()
    {
        if (currentAmount > 0)
        {
            StopCoroutine(Regenerate());
            isRegenerating = false;
            currentAmount -= 1;
            batteryUIObjects[currentAmount].SetActive(false);
            Debug.Log(currentAmount);
        }
    }



}
