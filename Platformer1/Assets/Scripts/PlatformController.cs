using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformController : MonoBehaviour
{
    public int amountOfPlayers = 2;

    private Tilemap tilemap;
    private Hashtable groundChecks = new Hashtable();

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();

        for(int i = 0; i< amountOfPlayers; i++)
        {
            groundChecks["Player" + i] = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("exit:" + collision.gameObject.name);
        groundChecks[collision.gameObject.name] = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player1") || collision.gameObject.name.Equals("Player2"))
        {
            Debug.Log("stay:" + collision.gameObject.name);

            groundChecks[collision.gameObject.name] = true;

            if (AllPlayersOnPlatform())
            {
                var tilePos = tilemap.WorldToCell(collision.gameObject.transform.position);
                Debug.Log("Disappear location:" + tilePos);

                tilemap.SetTile(tilePos, null); // Remove tile at 0,0,0
            }
        }
    }

    bool AllPlayersOnPlatform()
    {
        bool areAllInPlatform = true;

        for (int i = 0; i < amountOfPlayers; i++)
        {
            areAllInPlatform = areAllInPlatform && (bool) groundChecks["Player" + i];

            Debug.Log("Player" + i + " is " + areAllInPlatform);
        }

        return areAllInPlatform;
    }
}
