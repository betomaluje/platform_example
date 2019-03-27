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

        getTiles();
    }

    private Hashtable getTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] tiles = tilemap.GetTilesBlock(bounds);

        Hashtable allTiles = new Hashtable();

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = tiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y);
                    allTiles[new Vector3Int(x, y, 0)] = tile;
                }
            }
        }

        return allTiles;
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

                        Vector3Int position = tilemap.WorldToCell(hitPosition);
                        Vector3Int position2 = tilemap.LocalToCell(hitPosition);
                        Vector3 position3 = tilemap.LocalToWorld(
                            new Vector3(
                            hitPosition.x, hitPosition.y, 0)
                        );
                        Vector3 position4 = tilemap.WorldToLocal(hitPosition);

                        Debug.Log("player position:" + player.transform.position);
                        Debug.Log("hitPosition:" + hitPosition);
                        Debug.Log("position:" + position);
                        Debug.Log("position2:" + position2);
                        Debug.Log("position3:" + position3);
                        Debug.Log("position4:" + position4);
                        Debug.Log("position5:" + player.transform.localPosition);

                        /*TileBase tile = tilemap.GetTile(position);

                        Debug.Log("Disappear location:" + position + " tile " + tile);

                        if (tile != null) {
                            Debug.Log("Destroying tile");
                            Destroy(tile);
                        }*/
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
