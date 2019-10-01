using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject grass;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> islandParts = new List<GameObject>();
        for (int x = 0; x < 10; x++)
        {
            if (islandParts.Count == 0)
            {
                Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, Random.Range(4, 8) * 5);
                Vector3 location = new Vector3(Random.Range(0, 2), 0, Random.Range(0, 2));
                islandParts.Add(Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                islandParts[islandParts.Count - 1].transform.localScale = size;
            }
            else
            {
                Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, Random.Range(4, 8) * 5);
                Vector3 location = new Vector3(islandParts[islandParts.Count - 1].transform.position.x + size.x / 2 + islandParts[islandParts.Count - 1].transform.localScale.x /2, 0, islandParts[islandParts.Count - 1].transform.position.z);
                islandParts.Add(Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                islandParts[islandParts.Count - 1].transform.localScale = size;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
