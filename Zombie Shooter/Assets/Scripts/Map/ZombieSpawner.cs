using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private Map map;

    [SerializeField]
    private ScoreSystem scoreSystem;

    [SerializeField]
    private Transform zombieFolder;

    [Space]

    [SerializeField]
    private GameObject easyZombie;

    [SerializeField]
    private GameObject mediumZombie;

    [SerializeField]
    private GameObject hardZombie;

    [Space]

    [SerializeField]
    private Transform player;

    [SerializeField]
    private PlayerHealth playerHealth;

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
        if (!PlayerHealth.gameOver && !PauseMenuToggle.gamePaused)
        {
            // Spawns random diffeculty zombie based on the time the game has been running, on a random tile when the cooldown is done.
            if (Time.time > cooldown)
            {
                bool spawned = false;
                do
                {
                    Map.Tile spawnTile = map.tiles[Random.Range(0, map.tiles.Count)];
                    if (Vector2.Distance(new Vector2(player.position.x, player.position.z), spawnTile.location) > 8 && Vector2.Distance(new Vector2(player.position.x, player.position.z), spawnTile.location) < 50)
                    {
                        // The max random number increase as you survive making the change of a more diffecult zombie to spawn.
                        if (!spawnTile.wallUp && !spawnTile.wallMovingDown && !spawnTile.wallMovingUp)
                        {
                            float randomNumber = Random.Range(0, Time.timeScale);
                            if (randomNumber <= 10)
                            {
                                spawned = SpawnZombie(easyZombie, spawnTile);
                            }
                            else if (randomNumber <= 20)
                            {
                                spawned = SpawnZombie(mediumZombie, spawnTile);
                            }
                            else if (randomNumber <= 30)
                            {
                                spawned = SpawnZombie(hardZombie, spawnTile);
                            }
                        }
                    }
                }
                while (!spawned);
            }
        }
    }

    // Spawns the given zombie at the given location.
    bool SpawnZombie(GameObject zombie, Map.Tile spawnTile)
    {
        GameObject spawnedZombie = Instantiate(zombie, new Vector3(spawnTile.location.x, 1, spawnTile.location.z), new Quaternion(0, 0, 0, 0), zombieFolder);
        spawnedZombie.GetComponent<AIMove>().player = player;
        Enemy enemy = spawnedZombie.GetComponent<Enemy>();
        enemy.map = map;
        enemy.playerHealth = playerHealth;
        enemy.scoreSystem = scoreSystem;

        map.zombies.Add(spawnedZombie.transform);

        cooldown = Time.time + delay;
        return true;
    }
}
