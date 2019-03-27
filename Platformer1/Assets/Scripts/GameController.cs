using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private List<GameObject> cameras = new List<GameObject>();
    private List<PlayerController> players = new List<PlayerController>();
    private int playerIndex = 0;

    public void AddPlayer(GameObject player)
    {
        PlayerController playerController = player.transform.Find("Character").GetComponent<PlayerController>();
        if (playerController != null)
        {
            Debug.Log(playerController);
            players.Add(playerController);
        }

        GameObject camera = player.transform.Find("Camera").gameObject;

        if(camera != null)
        {
            Debug.Log(camera);
            cameras.Add(camera);
        }
    }

    public void Reset()
    {
        cameras.Clear();
        players.Clear();
    }

    public void Init()
    {
        TogglePlayers(playerIndex);
    }

    public void ChangePlayer()
    {
        Debug.Log("cameras: " + cameras.Count);
        Debug.Log("players: " + players.Count);

        if (players.Count > 0 && cameras.Count > 0)
        {
            playerIndex++;

            if (playerIndex >= players.Count)
            {
                playerIndex = 0;
            }

            TogglePlayers(playerIndex);

        }
    }

    void TogglePlayers(int activePlayer)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i == activePlayer)
            {
                players[i].SetIsCurrentPlayer(true);
                cameras[i].gameObject.SetActive(true);
            } 
            else
            {
                players[i].SetIsCurrentPlayer(false);
                cameras[i].gameObject.SetActive(false);
            }
        }
    }

}
