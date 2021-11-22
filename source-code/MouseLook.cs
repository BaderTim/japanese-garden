using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for camera movements
public class MouseLook : MonoBehaviour
{
    // Mouse sensitivity
    public static float mouseSensitivity = 100f;

    // Create a reference to the transform property of the player object to do position manipulation
    public Transform playerBody;

    // X-axis Rotation, default is 0f, camera is not rotated
    float xRotation = 0f;


    // Update is called once per frame
    void Update() 
    { 
        // Get mouse input for X, Y coordinates, multiplied by sensitivity, using deltaTime for frame rate indepence
        // Deltatime is the time that has passed between the previous update and the current one
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Invert mouseY so we can use the value properly for rotation
        xRotation -= mouseY;

        // Ceil the rotation at -90 and 90 degrees so the camera movement becomes more realistic
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotating the player object on X-Axis locally => looking up and down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotating the player around Z-axis => looking to left / right
        playerBody.Rotate(Vector3.up * mouseX);

      
    }
}
