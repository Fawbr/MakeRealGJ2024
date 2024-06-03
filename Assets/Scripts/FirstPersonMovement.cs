using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [Header("Camera Inputs")]
    [SerializeField] float cameraSensitivity = 100f;
    [SerializeField] Camera cam;
    float xRotation = 0f;

    [Header("CC Inputs")]
    [SerializeField] CharacterController controller;
    [SerializeField] float speed;
    
    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] float gravity;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gravMultiplier;
    Vector3 velocity;
    bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        MouseLook();
        Movement();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation =  Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()
    {    
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDir = transform.right * x + transform.forward * z;
        velocity.y += gravity * gravMultiplier * Time.deltaTime;
        controller.Move(moveDir * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}
