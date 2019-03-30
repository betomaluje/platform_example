using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : PlayerMovementController
{
    Rigidbody2D body;

    float vertical;
    float moveLimiter = 0.7f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        base.Update();

        // Gives a value between -1 and 1
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        if (horizontalMove != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontalMove *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontalMove, vertical * runSpeed);
    }
}
