using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is responsible for player's movement.
public class PlayerMove : MonoBehaviour
{
    // Referencing the character controller of our player object
    public CharacterController controller;

    // Mousespeed variable
    public float speed = 12f;

    // Update is called once per frame
    void Update()
    {
        // Get horizontal movement
        float x = Input.GetAxis("Horizontal");

        // Get vertical movement
        float z = Input.GetAxis("Vertical");


        // Create direction vector
        Vector3 moveVector = transform.right * x + transform.forward * z;


        // Call Move function to make the movement, 
        // Move vector is multiplied by the speed and deltaTime (to achieve frame rate indepence) 
        controller.Move(moveVector * speed * Time.deltaTime);


        // Handle gravity
        // Check if character is grounded
        if (controller.isGrounded == false)
        {   
            // Reset move vector
            moveVector = Vector3.zero;
            // Add gravity (direction vector (0,0,-1)) to move vector
            moveVector += Physics.gravity;
            // Apply move Vector, remeber to multiply by Time.delta
            controller.Move(moveVector * Time.deltaTime);
        }

    }
}
