using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;

    private void Start()
    {
        if (cameraPosition == null)
        {
            GameObject camPosObj = GameObject.Find("CameraPos");
            if (camPosObj != null)
            {
                cameraPosition = camPosObj.transform;
            }
            else
            {
                Debug.LogError("CameraPos not found under Player object. Please ensure it exists.");
            }
        }
    }

    private void Update()
    {
        if (cameraPosition != null)
        {
            transform.position = cameraPosition.position;
        }
    }
}
