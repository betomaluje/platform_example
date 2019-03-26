using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformController : MonoBehaviour
{
    public List<GameObject> players;

    private int amountOfPlayers = 2;

    private Tilemap tilemap;
    private Hashtable groundChecks = new Hashtable();

    private void Awake()
    {
        amountOfPlayers = players.Count;

        tilemap = GetComponent<Tilemap>();

        foreach(GameObject player in players)
        {
            groundChecks[player] = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundChecks[collision.gameObject] = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (GameObject player in players)
        {
            if (collision.gameObject == player)
            {
                groundChecks[player] = true;

                if (AllPlayersOnPlatform())
                {
                    Vector3 hitPosition = Vector3.zero;

                    foreach (ContactPoint2D hit in collision.contacts)
                    {
                        hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                        hitPosition.y = hit.point.y - 0.01f * hit.normal.y;

                        Debug.Log("Disappear location:" + hitPosition);

                        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                    }
                }

                break;
            }
        }
    }

    bool AllPlayersOnPlatform()
    {
        bool areAllInPlatform = true;

        foreach (GameObject player in players)
        {
            areAllInPlatform = areAllInPlatform && (bool)groundChecks[player];
        }

        return areAllInPlatform;
    }
}
