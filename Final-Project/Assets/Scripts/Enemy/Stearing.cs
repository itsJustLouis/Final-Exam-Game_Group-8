using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stearing : MonoBehaviour
{
    public abstract (float[] danger, float[] interest)

        GetStearing(float[] danger, float[] interest, AIData aiData);
}
