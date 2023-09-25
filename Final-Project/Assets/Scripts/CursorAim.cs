using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CursorAim : MonoBehaviour
{
    private Vector3 target;
    public GameObject Crosshairs;
    public GameObject Player;
    private void Start()
    {
        Cursor.visible = false;
    }


    private void Update()
    {


        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - Player.transform.position;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

       // Player.transform.rotation = Quaternion.EulerRotation(0, 0, rotationZ);
    }


}
