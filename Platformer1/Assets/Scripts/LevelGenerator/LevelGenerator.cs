using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameController gameController;

	public Texture2D map;

	public ColorToPrefab[] colorMappings;

	// Use this for initialization
	void Start () {
		GenerateLevel();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Change"))
        {
            Debug.Log("Changing player!");
            gameController.ChangePlayer();
        }
    }

    void GenerateLevel ()
	{
        gameController.Reset();

        for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				GenerateTile(x, y);
			}
		}

        gameController.Init();
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

                if (theNewObject.tag == "Player")
                {
                    gameController.AddPlayer(theNewObject);
                }

                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                break;
			}
		}
    }
	
}
