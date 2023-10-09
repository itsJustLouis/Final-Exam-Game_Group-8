using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RotateFlashLight : MonoBehaviour
{
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction;

        // Check if a Gamepad is connected
        if (Gamepad.current != null)
        {
            // Use the right stick for rotation
            direction = Gamepad.current.rightStick.ReadValue();
            if (direction.sqrMagnitude < 0.1)
                return; // Ignore small movements
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = mainCam.ScreenToWorldPoint(mousePos);

            direction = new Vector2(
                mousePos.x - transform.position.x,
                mousePos.y - transform.position.y
            );
        }

        transform.up = direction;
    }
}
