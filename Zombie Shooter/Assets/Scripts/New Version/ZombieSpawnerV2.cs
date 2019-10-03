using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerV2 : MonoBehaviour
{
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
                for (int maxLoops = 0; maxLoops < 100; maxLoops++)
                {
                    Vector3 spawnLocation = new Vector3(Random.Range(player.transform.position.x - 100, player.transform.position.x + 100),3, Random.Range(player.transform.position.z - 100, player.transform.position.z + 100));
                    if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(spawnLocation.x, spawnLocation.z)) > 10)
                    {
                        // The max random number increase as you survive making the change of a more diffecult zombie to spawn.
                        float randomNumber = Random.Range(0, Time.timeSinceLevelLoad);
                        if (randomNumber <= 20)
                        {
                            spawned = SpawnZombie(easyZombie, spawnLocation);
                            break;
                        }
                        else if (randomNumber <= 40)
                        {
                            spawned = SpawnZombie(mediumZombie, spawnLocation);
                            break;
                        }
                        else if (randomNumber <= 60)
                        {
                            spawned = SpawnZombie(hardZombie, spawnLocation);
                            break;
                        }
                    }
                }
            }
        }
    }

    // Spawns the given zombie at the given location.
    bool SpawnZombie(GameObject zombie, Vector3 spawnLocation)
    {
        GameObject spawnedZombie = Instantiate(zombie, spawnLocation, new Quaternion(0, 0, 0, 0), zombieFolder);
        spawnedZombie.GetComponent<AIMove>().player = player;
        EnemyV2 enemy = spawnedZombie.GetComponent<EnemyV2>();
        enemy.playerHealth = playerHealth;
        enemy.scoreSystem = scoreSystem;

        cooldown = Time.time + delay;
        return true;
    }
}
