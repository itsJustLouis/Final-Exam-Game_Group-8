using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlowingLight : MonoBehaviour
{
     public float minOuterRadius = 1.0f;     
    public float maxOuterRadius = 5.0f;  
    public float changeSpeed = 1.0f;    

    private Light2D spotlight; 
    private bool isGrowing = true; 

    void Start()
    {
        spotlight = GetComponent<Light2D>();

        spotlight.pointLightOuterRadius = minOuterRadius;
    }

    void Update()
    {
       if (isGrowing)
        {
            spotlight.pointLightOuterRadius += changeSpeed * Time.deltaTime;
            if (spotlight.pointLightOuterRadius >= maxOuterRadius)
            {
                spotlight.pointLightOuterRadius = maxOuterRadius;
                isGrowing = false;
            }
        }
        else
        {
            spotlight.pointLightOuterRadius -= (changeSpeed/2) * Time.deltaTime;
            if (spotlight.pointLightOuterRadius <= minOuterRadius)
            {
                spotlight.pointLightOuterRadius = minOuterRadius;
                isGrowing = true;
            }
        }
    }
}
