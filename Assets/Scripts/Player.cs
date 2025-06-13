using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    [Header("Game Objects")]
    CharacterController PlayerController;
    public Camera PlayerCam;

    [Header("Player Position")]
    public Vector3 updatePos;

    public float gravity;
    float walkSpd = 2.0f;
    float runSpd = 3.0f;

    bool playerCanMove = true;

    [Header("Camera Position")]
    float camSensitivity = 2.0f;
    float camRotationX = 0.0f; // Up & Down angle
    float camRotationY = 0.0f; // Left & Right angle

    float camxLimit = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveForward = transform.TransformDirection(Vector3.forward);
        Vector3 moveRight = transform.TransformDirection(Vector3.right);

        // Move character
        bool isRunning = Input.GetMouseButton(0);
        float charDisplacementX = playerCanMove ? (isRunning ? runSpd : walkSpd) * Input.GetAxis("Vertical") : 0;
        float charDisplacementZ = playerCanMove ? (isRunning ? runSpd : walkSpd) * Input.GetAxis("Horizontal") : 0;

        updatePos = (moveForward * charDisplacementX) + (moveRight * charDisplacementZ);
        
        // Keeps player grounded
        if (!PlayerController.isGrounded) updatePos.y -= gravity * Time.deltaTime;

        if (playerCanMove)
        {
            // Update camera angle
            camRotationY += Input.GetAxis("Mouse X") * camSensitivity;
            camRotationX += -Input.GetAxis("Mouse Y") * camSensitivity;
            camRotationX = Mathf.Clamp(camRotationX, -camxLimit, camxLimit);

            PlayerCam.transform.localRotation = Quaternion.Euler(camRotationX, 0, 0);
            transform.rotation = Quaternion.Euler(0, camRotationY, 0);

            // Update player position
            PlayerController.Move(updatePos * Time.deltaTime);
        }

        
    }
}
