using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 0;

    public float health = 20;
    public float maxHealth = 20;

    // Rigidbody component allows object to be controlled by physics
    private Rigidbody playerRb;
    private float movementX;
    private float movementY;


    // Start is called before the first frame update
    void Start()
    {
        // rb holds reference to the player object's rigidbody component
        playerRb = GetComponent<Rigidbody>();
        
    }
        
    // arg comes from Unity's Input System, which we applied to the player object
    // this function triggers whenever it receives an input: pressing WASD or moving joystick
    void OnMove(InputValue movementValue)
    {
        // get movement input data from playerobject and store it in a Vector2 variable
        Vector2 movementVector = movementValue.Get<Vector2>();

        // store those values in separate variables
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            health -= 5; 
        }

        if (health <= 0)
        {
            return;
        }
    }


    // FixedUpdate is recommended place to apply forces and change Rigidbody settings
    // FixedUpdate - 50 calls per second (frame independent) vs. Update - called every frame
    // LateUpdate - called every frame, after all Updates are called
    void FixedUpdate()
    {
        // Create new Vector3 variable, using movement values from OnMove, with y as 0
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // this only accepts a Vector3 variable, which is why we made one
        playerRb.AddForce(movement * playerSpeed);
    }
}
