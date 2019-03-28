using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    private int playerIndex = 0;

    private void Awake()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Change"))
        {
            ChangePlayer();
        }
    }

    public void AddPlayer(GameObject player)
    {
        player.transform.Find("Character").gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        players.Add(player);
    }

    public void Reset()
    {
        players.Clear();
    }

    public void Init()
    {
        TogglePlayers(playerIndex);
    }

    public void ChangePlayer()
    {
        if (players.Count > 0)
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

            GameObject character = players[i].transform.Find("Character").gameObject;

            Debug.Log("Changing "+ character .name + " " + i + " active: " + isActive);

            if (isActive)
            {
                character.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                character.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }

}
