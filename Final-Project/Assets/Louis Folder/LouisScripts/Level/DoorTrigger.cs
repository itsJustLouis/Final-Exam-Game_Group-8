using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] DoorHinge door;

    private void OnTriggerEnter2D(Collider2D light)
    {
        if (light.gameObject.tag == "TorchLight")
        {
            door.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D light)
    {
        if (light.gameObject.tag == "TorchLight")
        {
            door.CloseDoor();
        }

    }
}
