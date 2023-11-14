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
        //Debug.Log(currentBatteryLevel);
        if (currentBatteryLevel<5)
        {
            //currentBatteryLevel = maxBatteryLevel;
        }

    }

   public void DecreaseBatteryLevel()
    {
        if (Time.timeScale == 0f)
        {
            // If the gameis paused, dont decrease battery
            return;
        }
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

    public void IncreaseBatteryLevel()
    {

        if (currentBatteryLevel >= 5)
        {
            // Call this function when you want to decrease the battery level
            currentBatteryLevel = 5;
        }
        else
        {
            if (currentBatteryLevel <5)
            {
                currentBatteryLevel=5;

            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
           // Debug.Log("Player collided with a Battery object");
            // Add your logic here for what should happen when the player collides with the Battery
            other.gameObject.SetActive(false);
            IncreaseBatteryLevel();
        }
    }

}
