using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<PlayerController> players;
    public List<GameObject> cameras;

    private int playerIndex = 0;

    private void Awake()
    {
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
    }

    public void AddPlayer(GameObject player, int x, int y)
    {
        player.transform.position = new Vector2(x, y);

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

}
