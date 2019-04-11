using UnityEngine;

public class PlayerController : PlayerMovementController
{
    public ParticleSystem controlParticles;
    public CharacterController2D controller;
    private bool isJumping = false;

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
        if(horizontalMove != 0.0f)
        {
            gameObject.SendMessage("applyDamage", 0.05f, SendMessageOptions.DontRequireReceiver);
            controlParticles.Play();
        } else
        {
            // we instantiate the particles
            controlParticles.Stop();
        }

        // move player
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

}
