using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

	public Texture2D map;

	public ColorToPrefab[] colorMappings;

    public GameObject[] players;
    public Color[] playerColors;

    private int playersChanged = 0;

    // Use this for initialization
    void Start () 
    {
		GenerateLevel();
    }

    void GenerateLevel ()
	{
        //gameController.Reset();

        for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				GenerateTile(x, y);
			}
		}
    }

	void GenerateTile (int x, int y)
	{
		Color pixelColor = map.GetPixel(x, y);

		if (pixelColor.a == 0)
		{
			// The pixel is transparrent. Let's ignore it!
			return;
		}

		foreach (ColorToPrefab colorMapping in colorMappings)
		{
			if (colorMapping.color.Equals(pixelColor))
			{
                if(IsColorAPlayer(colorMapping.color))
                {
                    InstantiatePlayer(x, y);
                }
                else
                {
                    Vector2 position = new Vector2(x, y);
                    GameObject theNewObject = colorMapping.prefab;

                    Instantiate(theNewObject, position, Quaternion.identity, transform);
                }

                break;
			}
		}
    }

    void InstantiatePlayer(int x, int y)
    { 
        if(playersChanged < players.Length)
        {
            Debug.Log("Moving player " + playersChanged + " to: " + x + ", " + y);

            players[playersChanged].transform.position = new Vector2(x, y);

            playersChanged++;
        }
    }

    bool IsColorAPlayer(Color searchedColor)
    {
        foreach(Color color in playerColors)
        {
            if(color.Equals(searchedColor))
            {
                return true;
            }
        }

        return false;
    }

}
