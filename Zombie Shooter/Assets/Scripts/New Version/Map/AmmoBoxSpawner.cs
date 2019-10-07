using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AmmoBoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ammoBox;
    [SerializeField] private Transform player;
    [SerializeField] private float delay = 30;

    private float cooldown;

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime > cooldown)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(player.position.x - 100, player.position.x + 100), 3, Random.Range(player.position.z - 100, player.position.z + 100));
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnLocation, out hit, 5, 1))
            {
                spawnLocation = hit.position + new Vector3(0, 2, 0);
                GameObject justSpawned = Instantiate(ammoBox, spawnLocation, new Quaternion(0, 0, 0, 0), this.transform);
            }
        }
    }
}
