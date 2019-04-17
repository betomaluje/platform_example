using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem controlParticles;
    public Joystick joystick;

    private PlayerStatsController playerStats;
    private float horizontalMove = 0;

    private CharacterController2D controller;
    private bool isJumping = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        playerStats = GetComponent<PlayerStatsController>();
    }

    private void Update()
    {
        if(joystick.Horizontal >= .2f)
        {
            horizontalMove = playerStats.speed; // -1 is left
        } else if (joystick.Horizontal <= -.2f)
        {
            horizontalMove = -playerStats.speed; // -1 is left
        } else
        {
            horizontalMove = 0;
        }
    }

    public void Jump()
    {
        isJumping = true;
    }

    private void FixedUpdate()
    {
        if(horizontalMove != 0.0f || isJumping)
        {
            gameObject.SendMessage("applyDamage", 0.1f, SendMessageOptions.DontRequireReceiver);
            controlParticles.Play();
        }

        // move player
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

}
