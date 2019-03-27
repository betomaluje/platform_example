using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private List<GameObject> cameras = new List<GameObject>();
    private List<GameObject> players = new List<GameObject>();
    private int playerIndex = 0;

    public void AddPlayer(GameObject player)
    {
        GameObject playerController = player.transform.Find("Character").gameObject;

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
        Debug.Log("Changing player to: " + activePlayer);

        for (int i = 0; i < players.Count; i++)
        {
            bool isActive = i == activePlayer;

            Debug.Log("Changing player " + i + " active: " + isActive);
            Debug.Log(cameras[i]);

            PlayerController playerController = players[i].GetComponent<PlayerController>();
            playerController.enabled = isActive;
            cameras[i].SetActive(isActive);
        }
    }

}
