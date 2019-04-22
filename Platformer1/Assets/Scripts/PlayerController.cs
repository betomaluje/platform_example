﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem controlParticles;
    public Joystick joystick;

    private PlayerStatsController playerStats;
    private float horizontalMove = 0;
    private Weapon weapon;
    private GameObject weaponObject;

    private CharacterController2D controller;
    private bool isJumping = false;

    private void Awake()
    {
        weapon = null;
        controller = GetComponent<CharacterController2D>();
        playerStats = GetComponent<PlayerStatsController>();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (joystick.Horizontal >= .2f)
            {
                horizontalMove = playerStats.speed; // -1 is left
            }
            else if (joystick.Horizontal <= -.2f)
            {
                horizontalMove = -playerStats.speed; // -1 is left
            }
            else
            {
                horizontalMove = 0;
            }
        } else
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * playerStats.speed; // -1 is left

            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
            }
        }
       
        if (weapon && weaponObject && Input.GetButtonDown("Fire1"))
        {
            // we attack
            weapon.Attack(weaponObject);
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
            gameObject.SendMessage("applyDecaeDamage", SendMessageOptions.DontRequireReceiver);
            controlParticles.Play();
        }

        // move player
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

    public void updateWeapon(Weapon newWeapon, GameObject newWeaponObject)
    {       
        weapon = newWeapon;
        weaponObject = newWeaponObject;        
    }

}
