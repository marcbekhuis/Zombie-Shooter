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
            Vector3 location = new Vector3(2.1f * (int)Random.Range(0, map.mapSize.x) - (2.1f * (map.mapSize.x / 2)), 0, 2.1f * (int)Random.Range(0,map.mapSize.y) - (2.1f * (map.mapSize.y / 2)));
            Instantiate(zombie, location, new Quaternion(0, 0, 0, 0), this.transform);
        }
    }
}
