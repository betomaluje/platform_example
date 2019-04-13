using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem controlParticles;

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
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerStats.speed; // -1 is left

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if(horizontalMove != 0.0f || isJumping)
        {
            gameObject.SendMessage("applyDamage", 0.1f, SendMessageOptions.DontRequireReceiver);
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
