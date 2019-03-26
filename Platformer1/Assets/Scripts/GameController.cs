using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public List<PlayerController> players;
    public List<GameObject> cameras;

    private int playerIndex = 0;

    private void Awake()
    {
        TogglePlayers(playerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Change"))
        {
            ChangePlayer();
        }
    }

    void ChangePlayer()
    {
        playerIndex++;

        if(playerIndex >= players.Count)
        {
            playerIndex = 0;
        }

        TogglePlayers(playerIndex);
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
