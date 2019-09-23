using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject wall;

    [SerializeField]
    Map map;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < map.mapSize.x; x++)
        {
            for (int y = 0; y < map.mapSize.y; y++)
            {
                SpawnWall(x,y);
            }
        }
    }

    void SpawnWall(int x, int y)
    {
        if (Random.Range(0, 20) < 5)
        {
            Vector3 location = new Vector3(2.1f * x - (2.1f * (map.mapSize.x / 2)), 0, 2.1f * y - (2.1f * (map.mapSize.y / 2)));
            GameObject spawnedWall = Instantiate(wall, location, new Quaternion(0, 0, 0, 0), this.transform);
            map.amountOfWallsUp++;
            map.tiles.Add(new Map.Tile(spawnedWall,location,true,false,false));
        }
        else
        {
            Vector3 location = new Vector3(2.1f * x - (2.1f * (map.mapSize.x / 2)), -1.95f, 2.1f * y - (2.1f * (map.mapSize.y / 2)));
            GameObject spawnedWall = Instantiate(wall, location, new Quaternion(0, 0, 0, 0), this.transform);
            map.tiles.Add(new Map.Tile(spawnedWall, location, false,false,false));
        }
    }
}
