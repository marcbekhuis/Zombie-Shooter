using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private Map map;

    [SerializeField]
    private GameObject zombie;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float delay = 5;

    private float cooldown;

    private void Start()
    {
        cooldown = Time.time + delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > cooldown)
        {
            bool spawned = false;
            do
            {
                Map.Tile spawnTile = map.tiles[Random.Range(0, map.tiles.Count)];
                if (!spawnTile.wallUp && !spawnTile.wallMovingDown && !spawnTile.wallMovingUp)
                {
                    GameObject spawnedZombie = Instantiate(zombie, new Vector3(spawnTile.location.x, 1, spawnTile.location.z), new Quaternion(0, 0, 0, 0), this.transform);
                    spawnedZombie.GetComponent<AIMove>().player = player;
                    spawnedZombie.GetComponent<Enemy>().map = map;

                    map.zombies.Add(spawnedZombie.transform);

                    cooldown = Time.time + delay;
                    spawned = true;
                }
            }
            while (!spawned);
        }
    }
}
