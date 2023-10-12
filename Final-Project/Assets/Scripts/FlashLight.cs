using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashLight : MonoBehaviour
{
    public int maxBatteryLevel = 5;
    public int currentBatteryLevel;
    public Slider batterySlider; // Reference to the UI Slider

    void Start()
    {
        currentBatteryLevel = maxBatteryLevel;
        batterySlider.maxValue = maxBatteryLevel;
        batterySlider.value = currentBatteryLevel;
    }

    void Update()
    {
        
        batterySlider.value = currentBatteryLevel;
        Debug.Log(currentBatteryLevel);
        if (currentBatteryLevel<5)
        {
            //currentBatteryLevel = maxBatteryLevel;
        }
    }

   public void DecreaseBatteryLevel()
    {
      
        if (currentBatteryLevel <= 0)
        {
            // Call this function when you want to decrease the battery level
            currentBatteryLevel = 0;
        }
        else
        {
            if (currentBatteryLevel>0)
            {
                currentBatteryLevel--;

            }
        }

    }


}
