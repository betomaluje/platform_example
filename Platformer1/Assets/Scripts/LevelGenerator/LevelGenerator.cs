using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameController gameController;

    public Texture2D map;

	public ColorToPrefab[] colorMappings;

    // Use this for initialization
    void Start () 
    {
		GenerateLevel();
    }

    void GenerateLevel ()
	{
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
                Vector2 position = new Vector2(x, y);
                GameObject theNewObject = colorMapping.prefab;

                if(theNewObject == null)
                {
                    break;
                }

                if (theNewObject.CompareTag("Player"))
                {
                    // need to move the proper gameobjects already instantiated instead of instantiate them again                 
                    theNewObject.transform.position = position;
                    gameController.AddPlayer(theNewObject);
                }
                else if(theNewObject.CompareTag("Finish"))
                {
                    theNewObject.transform.position = position;
                }
                else
                {
                    Instantiate(theNewObject, position, Quaternion.identity, transform);
                }

                break;
			}
		}
    }
}
