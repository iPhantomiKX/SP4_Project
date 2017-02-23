using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour {

    public Map[] maps;
    public int mapIndex;

    public Transform background;
    public Transform tilePrefab;
    public Transform wallPrefab;
   // public Transform MonsterPrefab;

    [Range(0,1)]
    public float outlinePercent;
    public float tileSize;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;
    List<Coord> allOpenCoord;
    Queue<Coord> shuffledOpenTileCoords;
  
    Transform[,] tileMap;

    Map currentMap;

    // A* stuff
    Node[,] grid;
    // public Transform Player;


    public void GenerateMap()
    {
        currentMap = maps[mapIndex];

        tileMap = new Transform[currentMap.mapSize.x, currentMap.mapSize.y];

        // A*
        grid = new Node[currentMap.mapSize.x, currentMap.mapSize.y];

        // Generating Coords
        allTileCoords = new List<Coord>();
        for(int x =0; x<currentMap.mapSize.x; x++)
        {
            for(int y =0; y<currentMap.mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));

                // A*
                grid[x, y] = new Node(Node.tile_type.FLOOR, CoordToPosition(x, y), new Vector3(x, y, 0));
            }
        }
        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), currentMap.seed));

        // Create Map Holders
        string holderName4 = "Generated BackGround";
        if (transform.FindChild(holderName4))
        {
            DestroyImmediate(transform.FindChild(holderName4).gameObject);
        }
        Transform mapHolder4 = new GameObject(holderName4).transform;
        mapHolder4.parent = transform;

        string holderName = "Generated Tile";
        if(transform.FindChild(holderName))
        {
            DestroyImmediate(transform.FindChild(holderName).gameObject);
        }
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        string holderName2 = "Generated wall";
        if (transform.FindChild(holderName2))
        {
            DestroyImmediate(transform.FindChild(holderName2).gameObject);
        }
        Transform mapHolder2 = new GameObject(holderName2).transform;
        mapHolder2.parent = transform;


        //string holderName3 = "Generated Monsters";
        //if (transform.FindChild(holderName3))
        //{
        //    DestroyImmediate(transform.FindChild(holderName3).gameObject);
        //}
        //Transform mapHolder3 = new GameObject(holderName3).transform;
        //mapHolder3.parent = transform;



        // Spawning Tiles
        for (int x =0 ; x < currentMap.mapSize.x; x++)
        {
            for (int y=0; y<currentMap.mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent) * tileSize;
                newTile.parent = mapHolder;
                tileMap[x, y] = newTile;
            }
        }

        // Spawing walls
        bool[,] wallMap = new bool[(int)currentMap.mapSize.x, (int)currentMap.mapSize.y];
        int wallCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y * currentMap.wallPercent);

        int currentwallCount = 0;
        int outlineWallCount = 0;

        allOpenCoord = new List<Coord>(allTileCoords);

        // spawn the outline wall
        for (int x = 0; x < currentMap.mapSize.x; x++)
        {
            for (int y = 0; y < currentMap.mapSize.y; y++)
            {
                Coord XY = new Coord(x, y);

                if (x == 0 || x == currentMap.mapSize.x - 1)
                {

                    Vector3 wallPosition = CoordToPosition(XY.x, XY.y);
                    Transform newwall = Instantiate(wallPrefab, wallPosition + Vector3.forward * -0.5f, Quaternion.identity) as Transform;
                    newwall.localScale = Vector3.one /* * (1 - outlinePercent)*/ * tileSize;
                    newwall.parent = mapHolder2;

                    allOpenCoord.Remove(XY);

                    wallMap[XY.x, XY.y] = true;

                    outlineWallCount++;

                    // A*
                    grid[x, y].SetType(Node.tile_type.WALL);

                }
                else if (y == 0 || y == currentMap.mapSize.y - 1)
                {
                    Vector3 wallPosition = CoordToPosition(XY.x, XY.y);
                    Transform newwall = Instantiate(wallPrefab, wallPosition + Vector3.forward * -0.5f, Quaternion.identity) as Transform;
                    newwall.localScale = Vector3.one /* * (1 - outlinePercent)*/ * tileSize;
                    newwall.parent = mapHolder2;

                    allOpenCoord.Remove(XY);

                    wallMap[XY.x, XY.y] = true;

                    outlineWallCount++;

                    // A*
                    grid[x, y].SetType(Node.tile_type.WALL);
                }
            }
        }

       

        // randomly spawning
        for (int i = 0; i < wallCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            if (wallMap[randomCoord.x, randomCoord.y] != true)
            {
                wallMap[randomCoord.x, randomCoord.y] = true;
                currentwallCount++;

                if ((randomCoord != currentMap.mapCentre) && MapIsFullyAccessible(wallMap, currentwallCount + outlineWallCount))
                {
                    Vector3 wallPosition = CoordToPosition(randomCoord.x, randomCoord.y);
                    Transform newwall = Instantiate(wallPrefab, wallPosition + Vector3.forward * -0.5f, Quaternion.identity) as Transform;
                    newwall.localScale = Vector3.one /* * (1 - outlinePercent)*/ * tileSize;
                    newwall.parent = mapHolder2;

                    allOpenCoord.Remove(randomCoord);

                    // A*
                    grid[randomCoord.x, randomCoord.y].SetType(Node.tile_type.WALL);
                }
                else
                {
                    wallMap[randomCoord.x, randomCoord.y] = false;
                    currentwallCount--;
                }
            }
        }

     
        // spwaning Monster
        shuffledOpenTileCoords = new Queue<Coord>(Utility.ShuffleArray(allOpenCoord.ToArray(), currentMap.seed));


        //int currentMonsterCount = 0;

        //for (int i = 0; i < currentMap.MonsterCount; i++)
        //{
        //    Coord randomCoord = GetRandomOpenTile();

        //    currentMonsterCount++;

        //    if ((randomCoord != currentMap.mapCentre))
        //    {
        //        Vector3 newPosition = CoordToPosition(randomCoord.x, randomCoord.y);
        //        Transform newMonster = Instantiate(MonsterPrefab, newPosition + Vector3.forward * -1f, Quaternion.identity) as Transform;
        //        newMonster.localScale = Vector3.one /* * (1 - outlinePercent)*/ * tileSize;
        //        newMonster.parent = mapHolder3;

        //        allOpenCoord.Remove(randomCoord);
        //    }
        //    else
        //    {
        //        currentMonsterCount--;
        //    }
        //}


        // Generate BackGround
        Transform newBackground = Instantiate(background, new Vector3(0,0,1), Quaternion.identity) as Transform;
        newBackground.localScale = new Vector3(currentMap.mapSize.x, currentMap.mapSize.y, 0) * tileSize;
        newBackground.parent = mapHolder4;
        
    }

    public Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-currentMap.mapSize.x / 2f + 0.5f + x, -currentMap.mapSize.y / 2f + 0.5f + y, 0) * tileSize;
    }

    public Vector3 PositionToCoord(Vector3 other)
    {
        Vector3 temp = (new Vector3( other.x,other.y,0) / tileSize);
        temp.x += (currentMap.mapSize.x / 2f);
        temp.y += (currentMap.mapSize.y / 2f);
        temp.z = 0;
        return temp;
    }

    bool MapIsFullyAccessible(bool[,] wallMap, int currentwallCount)
    {
        bool[,] mapFlags = new bool[wallMap.GetLength(0), wallMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(currentMap.mapCentre);
        mapFlags[currentMap.mapCentre.x, currentMap.mapCentre.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for(int x = -1; x <= 1; x++)
            {
                for(int y= -1; y<= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < wallMap.GetLength(0) && neighbourY >= 0 && neighbourY < wallMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !wallMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessinleTileCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y - currentwallCount);
        return targetAccessinleTileCount == accessibleTileCount;
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    //public Coord GetRandomOpenTile()
    //{
    //    Coord randomCoord = shuffledOpenTileCoords.Dequeue();
    //    shuffledOpenTileCoords.Enqueue(randomCoord);
    //    return randomCoord;
    //}

    public Transform GetRandomOpenTile()
    {
        Coord randomCoord = shuffledOpenTileCoords.Dequeue();
        shuffledOpenTileCoords.Enqueue(randomCoord);
        allOpenCoord.Remove(randomCoord);
        return tileMap[randomCoord.x, randomCoord.y];
    }


    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord (int _x , int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }

    }

    [System.Serializable]
    public class Map
    {
        public Coord mapSize;
        [Range(0, 1)]
        public float wallPercent;

        // public int MonsterCount;
        public int seed;

        public Coord mapCentre
        {
            get
            {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }
    }




	// Use this for initialization
    void Awake()
    {
        GenerateMap();
    }

	void Start () {
        GenerateMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // A* stuff below ////////////////////////////////////////////////////////////
    // public List<Node> path;

    public List<Node> GetNeighbours(Node node, bool fourWay)
    {
        List<Node> neighbours = new List<Node>();

        int NDIR;
        int[] iDir;
        int[] jDir; 

        if (fourWay)
        {
            NDIR = 4;
            int[] x = { 1, 0, -1, 0 };
            int[] y = { 0, 1, 0, -1 };
            
            iDir = x;
            jDir = y;
        }
        else
        {
            NDIR = 8;
            int[] x = { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] y = { 0, 1, 1, 1, 0, -1, -1, -1 };

            iDir = x;
            jDir = y;
        }

        for (int i = 0; i < NDIR ; i++)
        {
            int checkX = (int)node.tilePosition.x + iDir[i];
            int checkY = (int)node.tilePosition.y + jDir[i];

            if (checkX >= 0 && checkX < currentMap.mapSize.x && checkY >= 0 && checkY < currentMap.mapSize.y) // if it is inside the map
            {
                neighbours.Add(grid[checkX, checkY]);
            }
            
        }
        return neighbours;
    }
    
    public Node NodeFromWorldPoint(Vector3 WorldPosition)
    {
        Vector3 TilePosition = PositionToCoord(WorldPosition);
        return grid[(int)TilePosition.x, (int)TilePosition.y];
    }

    public Node NodeFromTilePoint(Vector3 TilePosition)
    {
        return grid[(int)TilePosition.x, (int)TilePosition.y];
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawWireCube(transform.position, new Vector3(currentMap.mapSize.x, currentMap.mapSize.y, 0) * tileSize);

    //    if (grid != null)
    //    {
    //        Node Player_node = NodeFromWorldPoint(Player.transform.position);
    //        foreach (Node n in grid)
    //        {
    //            Gizmos.color = (n.Type == Node.tile_type.FLOOR) ? Color.white : Color.blue;
    //            if (Player_node == n)
    //            {
    //                Gizmos.color = Color.red;
    //            }

    //            Gizmos.DrawCube(n.worldPosition, new Vector3(1, 1, 0) * tileSize);
    //        }
    //    }
    //}
}
