using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// These videos take long to make so I hope this helps you out and if you want to help me out you can by leaving a like and subscribe, thanks!
 
public class Movement : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    [SerializeField] private bool cursorLock = true;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float Speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;  
    private float velocityY;
    private bool isGrounded;
 
    private float cameraCap;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;
    
    private CharacterController controller;
    private Vector2 currentDir;
    private Vector2 currentDirVelocity;
    private Vector3 velocity;
 
    void Start()
    {
        controller = GetComponent<CharacterController>();
 
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }
 
    void Update()
    {
        UpdateMouse();
        UpdateMove();
    }
 
    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
 
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
 
        cameraCap -= currentMouseDelta.y * mouseSensitivity;
 
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);
 
        playerCamera.localEulerAngles = Vector3.right * cameraCap;
 
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
 
    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
 
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
 
        velocityY += gravity * 2f * Time.deltaTime;
 
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed + Vector3.up * velocityY;
 
        controller.Move(velocity * Time.deltaTime);
 
        // if(isGrounded! && controller.velocity.y < -1f)
        // {
        //     velocityY = -200f;
        // }
    }
}