using UnityEngine;

public class PlayerController : PlayerMovementController
{
    public CharacterController2D controller;
    bool isJumping = false;

    private void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        // move player
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

}
