using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    [SerializeField]
    private GameObject kidZombie;

    [SerializeField]
    private GameObject giantZombie;

    [Space]

    [SerializeField]
    private Transform player;

    [SerializeField]
    private PlayerHealthV2 playerHealth;

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
        if (!PlayerHealth.gameOver && !PauseMenuToggle.gamePaused && !WinGame.wonGame)
        {
            // Spawns random diffeculty zombie based on the time the game has been running, on a random tile when the cooldown is done.
            if (Time.time > cooldown)
            {
                bool spawned = false;
                for (int maxLoops = 0; maxLoops < 100; maxLoops++)
                {
                    if (Random.Range(0, Time.timeSinceLevelLoad) < 320)
                    {
                        Vector3 spawnLocation = new Vector3(Random.Range(player.transform.position.x - 100, player.transform.position.x + 100), 3, Random.Range(player.transform.position.z - 100, player.transform.position.z + 100));
                        NavMeshHit hit;
                        if (NavMesh.SamplePosition(spawnLocation, out hit, 10, 1))
                        {
                            spawnLocation = hit.position + new Vector3(0, 2, 0);
                            if (Random.Range(0, 100) < 90)
                            {
                                if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(spawnLocation.x, spawnLocation.z)) > 30 && !Physics.CheckBox(spawnLocation, easyZombie.transform.localScale / 2))
                                {
                                    // The max random number increase as you survive making the change of a more diffecult zombie to spawn.
                                    float randomNumber = Random.Range(0, Time.timeSinceLevelLoad);
                                    if (randomNumber < 80)
                                    {
                                        spawned = SpawnZombie(easyZombie, spawnLocation);
                                        break;
                                    }
                                    else if (randomNumber < 160)
                                    {
                                        spawned = SpawnZombie(mediumZombie, spawnLocation);
                                        break;
                                    }
                                    else if (randomNumber < 240)
                                    {
                                        spawned = SpawnZombie(hardZombie, spawnLocation);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                // Spawns A Kid Zombie.
                                if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(spawnLocation.x, spawnLocation.z)) > 30 && !Physics.CheckBox(spawnLocation, new Vector3(1, 0.25f, 1)))
                                {
                                    spawned = SpawnZombie(kidZombie, spawnLocation);
                                    break;
                                }
                            }
                        }
                    }
                    else if (Random.Range(0, 100) < 5)
                    {
                        // Spawns A Giant Zombie.
                        Vector3 spawnLocation = new Vector3(Random.Range(player.transform.position.x - 100, player.transform.position.x + 100), 7, Random.Range(player.transform.position.z - 100, player.transform.position.z + 100));
                        NavMeshHit hit;
                        if (NavMesh.SamplePosition(spawnLocation, out hit, 15, 1))
                        {
                            spawnLocation = hit.position + new Vector3(0, 5, 0);
                            if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(spawnLocation.x, spawnLocation.z)) > 50 && !Physics.CheckBox(spawnLocation, new Vector3(2, 4, 2)))
                            {
                                spawned = SpawnZombie(giantZombie, spawnLocation);
                                break;
                            }
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
