using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowTank : MonoBehaviour
{
    public Transform objectToFollow;
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    private void Update()
    {
        if (objectToFollow != null)
        {
            // Offset the position by a desired amount in the vertical direction (y-axis)
            Vector3 offset = new Vector3(-0.05f, 0.8f, 0f);
            rectTransform.anchoredPosition = objectToFollow.localPosition + offset;
        }
    }

}
