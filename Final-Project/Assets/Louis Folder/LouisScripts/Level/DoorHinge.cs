using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHinge : MonoBehaviour
{
    HingeJoint2D hingeJoint2D;
    JointAngleLimits2D openDoorLimits;
    JointAngleLimits2D closeDoorLimits;

    private void Awake()
    {
        transform.Find("hinge").GetComponent<HingeJoint2D>();
        openDoorLimits = hingeJoint2D.limits;
        closeDoorLimits = new JointAngleLimits2D { min = 0, max = 0};
        CloseDoor();
    }

    public void OpenDoor()
    {
        hingeJoint2D.limits = openDoorLimits;
    }

    public void CloseDoor()
    {
        hingeJoint2D.limits = closeDoorLimits;
    }
}
