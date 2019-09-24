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
        Vector2 location = new Vector3(2.1f * x - (2.1f * (map.mapSize.x / 2)), 2.1f * y - (2.1f * (map.mapSize.y / 2)));
        if(Random.Range(0, 20) < 15 || Vector2.Distance(new Vector2(map.player.position.x, map.player.position.z), location) < 3)
        {
            GameObject spawnedWall = Instantiate(wall, new Vector3(location.x, -1.95f, location.y), new Quaternion(0, 0, 0, 0), this.transform);
            map.tiles.Add(new Map.Tile(spawnedWall, new Vector3(location.x, -1.95f, location.y), false,false,false));
        }
        else
        {
            GameObject spawnedWall = Instantiate(wall, new Vector3(location.x, 0, location.y), new Quaternion(0, 0, 0, 0), this.transform);
            map.amountOfWallsUp++;
            map.tiles.Add(new Map.Tile(spawnedWall, new Vector3(location.x, 0, location.y), true, false, false));
        }
    }
}
