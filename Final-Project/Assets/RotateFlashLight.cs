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

        // Always use the mouse position for rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCam.ScreenToWorldPoint(mousePos);

        direction = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
        );

        // Check if a Gamepad is connected
        if (Gamepad.current != null)
        {
            // Use the right stick for rotation
            Vector2 gamepadDirection = Gamepad.current.rightStick.ReadValue();
            if (gamepadDirection.sqrMagnitude >= 0.1)
            {
                // Use gamepad direction if there's significant movement
                direction = gamepadDirection;
            }
        }

        transform.up = direction;
    }



}
