using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformController : MonoBehaviour
{
    public int amountOfPlayers = 2;
    private int currentPlayers;

    private Tilemap tilemap;

    private void Awake()
    {
        currentPlayers = amountOfPlayers;
        tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") || collision.gameObject.name.Equals("Player2"))
        {
            Debug.Log("OnCollisionEnter2D: " + collision.gameObject.name + " players here: " + currentPlayers);
            currentPlayers++;

            if (currentPlayers >= amountOfPlayers)
            {
                Debug.Log("Disappear!");
                Vector3Int currentCell = tilemap.WorldToCell(transform.position);
                tilemap.SetTile(currentCell, null); // Remove tile at 0,0,0
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollisionExit2D: " + collision.gameObject.name + " players here: " + amountOfPlayers);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}
