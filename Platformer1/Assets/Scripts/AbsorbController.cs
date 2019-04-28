﻿using UnityEngine;
using System.Collections;

public class AbsorbController : MonoBehaviour
{
    public ParticleSystem controlParticles;

    private bool canControl = false;

    private GameObject otherPlayer = null;
    private PlayerController playerController;
    private GameController gameController;

    void Awake()
    {
        GameObject gameControllerObject = GameObject.Find("GameController").gameObject;

        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        otherPlayer = null;
        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canControl = true;
            otherPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canControl = false;
            otherPlayer = null;
        }
    }

    private void Update()
    {
        if(Application.platform != RuntimePlatform.Android)
        {
            if (CanControl() && Input.GetButtonDown("Absorb"))
            {
                ControlPlayer();
            }
        }
    }

    public void Absorb()
    {
        bool absorbClicked = true;

        if (Application.platform == RuntimePlatform.Android)
        {
            absorbClicked = true;
        } else
        {
            absorbClicked = Input.GetButtonDown("Absorb");
        }

        if (CanControl())
        {
            ControlPlayer();
        }
    }

    private bool CanControl()
    {
        return otherPlayer != null && playerController.enabled && canControl;
    }

    private void ControlPlayer()
    {
        if(otherPlayer != null)
        {
            gameObject.SendMessage("AnimationAbsorb", SendMessageOptions.DontRequireReceiver);

            // we instantiate the particles
            ParticleSystem absorbParticles = Instantiate(controlParticles, transform.position, transform.rotation, transform);
            Destroy(absorbParticles.gameObject, 2);                        

            gameController.ChangePlayer();

            // we deactivate my camera and player controller
            GameObject myCamera = transform.parent.Find("Camera").gameObject;            
            myCamera.gameObject.SetActive(false);

            GameController.ToggleScripts(gameObject, false);
            GameController.ToggleScripts(otherPlayer, true);            

            // we set active the other character's camera
            GameObject otherCamera = otherPlayer.transform.parent.Find("Camera").gameObject;            
            otherCamera.gameObject.SetActive(true);
        }
    }    
}
