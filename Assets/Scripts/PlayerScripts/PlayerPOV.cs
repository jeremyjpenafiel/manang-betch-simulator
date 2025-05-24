using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPOV : MonoBehaviour
{
    [SerializeField] private float downwardLookLimit = 30f;
    [SerializeField] private float upwardLookLimit = -90;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private Transform orientation;
    private float xRotation;
    private float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //find orientation object under Player object
        if (orientation == null)
        {
            orientation = GameObject.Find("Orientation").transform;
        }

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, upwardLookLimit, downwardLookLimit);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
