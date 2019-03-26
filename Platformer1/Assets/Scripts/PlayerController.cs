using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    float horizontalMove = 0;
    bool isJumping = false;

    bool canMove = false;

    private void Update()
    {
        if(canMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // move player
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

    public void SetIsCurrentPlayer(bool canMove)
    {
        this.canMove = canMove;
    }

}
