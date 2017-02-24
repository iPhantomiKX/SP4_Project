using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {


    public Level[] MapLevel;
    public AI_Scripts Monster;

    Level currentMapLevel;
    int currentMapLevelIndex;

    int monsterCount;
    //int monsterRemainingAlive;

    MapGeneration map;


    [System.Serializable]
    public class Level
    {
        public int monsterCount;
    }

    void NextMap()
    {
        currentMapLevelIndex++;
        currentMapLevel = MapLevel[currentMapLevelIndex - 1];

        monsterCount = currentMapLevel.monsterCount;
    }


    void GenerateMonster()
    {
        while (monsterCount > 0)
        {
            Transform RandomTile = map.GetRandomOpenTile();
            AI_Scripts newMonster = Instantiate(Monster, RandomTile.position + Vector3.forward * -1, Quaternion.identity) as AI_Scripts;
            newMonster.transform.parent = gameObject.transform;
            monsterCount--;
        }
    }

	// Use this for initialization
	void Start () {
        map = FindObjectOfType<MapGeneration>();
        NextMap();
	}
	
	// Update is called once per frame
	void Update () {
        GenerateMonster();
    }
}
