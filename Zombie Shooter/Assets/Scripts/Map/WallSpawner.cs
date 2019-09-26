using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject wall;

    // Has all the information about the map.
    [SerializeField]
    Map map;

    // is the parent of all the walls when they are spawned.
    [SerializeField]
    private Transform wallFolder;

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
        // Spawns a wall thats up or down and gives information to the map script where the tile is, if its up or down, the walls material.
        Vector2 location = new Vector3(2.1f * x - (2.1f * (map.mapSize.x / 2)), 2.1f * y - (2.1f * (map.mapSize.y / 2)));
        if(Random.Range(0, 20) < 15 || Vector2.Distance(new Vector2(map.player.position.x, map.player.position.z), location) < 3)
        {
            GameObject spawnedWall = Instantiate(wall, new Vector3(location.x, -1.95f, location.y), new Quaternion(0, 0, 0, 0), wallFolder);
            Material material = spawnedWall.GetComponent<MeshRenderer>().material;
            map.tiles.Add(new Map.Tile(spawnedWall, new Vector3(location.x, -1.95f, location.y), false,false,false,material));
            material.color = new Color(0,0,1);
        }
        else
        {
            GameObject spawnedWall = Instantiate(wall, new Vector3(location.x, 0, location.y), new Quaternion(0, 0, 0, 0), wallFolder);
            map.amountOfWallsUp++;
            Material material = spawnedWall.GetComponent<MeshRenderer>().material;
            map.tiles.Add(new Map.Tile(spawnedWall, new Vector3(location.x, 0, location.y), true, false, false, material));
            material.color = new Color(0, 0, 0.1f);
        }
    }
}
