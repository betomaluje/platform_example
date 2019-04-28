﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<PlayerController> players;
    public List<GameObject> cameras;
    public Button absorbButton;

    private int playerIndex = 0;

    private void Awake()
    {
        absorbButton.onClick.AddListener(InvokeAbsorb);
        TogglePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Change"))
        {
            ChangePlayer();
            TogglePlayers();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void AddPlayer(GameObject player)
    {
        PlayerController playerController = player.GetComponentInChildren<PlayerController>();

        if (playerController != null)
        {
            players.Add(playerController);
        }

        GameObject camera = player.transform.Find("Camera").gameObject;

        if(camera != null)
        {
            cameras.Add(camera);
        }

    }

    public void ChangePlayer()
    {
        playerIndex++;

        if (playerIndex >= players.Count)
        {
            playerIndex = 0;
        }
    }

    void TogglePlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i == playerIndex)
            {
                players[i].enabled = true;
                cameras[i].gameObject.SetActive(true);
            }
            else
            {
                players[i].enabled = false;
                cameras[i].gameObject.SetActive(false);
            }
        }
    }

    public void InvokeAbsorb()
    {
        players[playerIndex].gameObject.GetComponent<AbsorbController>().Absorb();
    }

    public static void ToggleScripts(GameObject gameObject, bool enabled)
    {
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = enabled;
        }

        if (gameObject.GetComponent<Rigidbody2D>())
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = !enabled? RigidbodyType2D.Static: RigidbodyType2D.Dynamic;
        }
    }

}
