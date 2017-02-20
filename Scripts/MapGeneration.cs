using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Coord
{
    public int x;
    public int y;

    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static bool operator ==(Coord c1, Coord c2)
    {
        return c1.x == c2.x && c1.y == c2.y;
    }

    public static bool operator !=(Coord c1, Coord c2)
    {
        return !(c1 == c2);
    }

}

public class MapGeneration : MonoBehaviour {

    public Transform tilePrefab;
    public Transform obstaclePrefab;

    [Range(1, 32)] public int mapSizeX, mapSizeY;
    public Vector2 tileSize;

    [Range(0,1)] public float outlinePercent;
    [Range(0,1)] public float obstaclePercent;

    public int seed = 10;



    List<Coord> tileCoords;
    Queue<Coord> shuffledTileCoords;
    Coord mapCenter;



    public void GenerateMap()
    {
        tileCoords = new List<Coord>();
        for(int x = 0; x < mapSizeX; x++)
        {
            for(int y = 0; y < mapSizeX; y++)
            {
                tileCoords.Add(new Coord(x, y));
            }
        }
        mapCenter = new Coord((mapSizeX - 1) / 2, (mapSizeY - 1) / 2);

        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(tileCoords.ToArray(), seed));

        string holderName = "Generated Map";
        if(transform.FindChild(holderName))
        {
            DestroyImmediate(transform.FindChild(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;


        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }
        }

        bool[,] obstacleMap = new bool[mapSizeX, mapSizeY];
        int obstacleCount = (int)(mapSizeX * mapSizeY * obstaclePercent);
        int currentObstacleCount = 0;

        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();

            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;

            if ((randomCoord != mapCenter) && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
            {
                Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.forward * -0.5f, Quaternion.identity) as Transform;
                newObstacle.parent = mapHolder;
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }
    }

    Vector3 CoordToPosition(int x, int y)
    {
        Vector3 r = new Vector3(
            (x - (mapSizeX - 1) * 0.5f) * tileSize.x,
            (y - (mapSizeY - 1) * 0.5f) * tileSize.y,
            0f);
        return r;
        //return new Vector3(-mapSize.x / 2 + 0.5f + x, -mapSize.y / 2 + 0.5f + y, 0);
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(mapCenter);
        mapFlags[mapCenter.x, mapCenter.y] = true;


        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for(int x = -1; x <= 1; ++x)
            {
                for(int y= -1; y<= 1; ++y)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                ++accessibleTileCount;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessinleTileCount = mapSizeX * mapSizeY - currentObstacleCount;
        return targetAccessinleTileCount == accessibleTileCount;
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

	// Use this for initialization
	void Start () {
	GenerateMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
