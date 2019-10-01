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
        Vector2Int islandSize = new Vector2Int(Random.Range(3,13), Random.Range(3, 13));
        GameObject[,] islandParts = new GameObject[islandSize.x,islandSize.y];
        for (int x = 0; x < islandSize.x ; x++)
        {
            for (int y = 0; y < islandSize.y; y++)
            {
                if (x == 0 && y == 0)
                {
                    Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, Random.Range(4, 8) * 5);
                    Vector3 location = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                    islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                    islandParts[x, y].transform.localScale = size;
                }
                else if (x != 0 && y == 0)
                {
                    Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, Random.Range(4, 8) * 5);
                    Vector3 location = new Vector3(islandParts[x - 1, y].transform.position.x + size.x / 2 + islandParts[x - 1, y].transform.localScale.x / 2, 0, this.transform.position.z);
                    islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                    islandParts[x, y].transform.localScale = size;
                }
                else if (x == 0 && y != 0)
                {
                    Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, Random.Range(4, 8) * 5);
                    Vector3 location = new Vector3(this.transform.position.x, 0, islandParts[x, y - 1].transform.position.z + size.z / 2 + islandParts[x, y - 1].transform.localScale.z / 2);
                    islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                    islandParts[x, y].transform.localScale = size;
                }
            }
        }

        //List<GameObject> islandParts = new List<GameObject>();
        //if (islandParts.Length == 0)
        //{
        //    Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, Random.Range(4, 8) * 5);
        //    Vector3 location = new Vector3(Random.Range(0, 2), 0, Random.Range(0, 2));
        //    islandParts[2, 2] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
        //    islandParts[2, 2].transform.localScale = size;
        //}
        //else
        //{
        //    Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, Random.Range(4, 8) * 5);
        //    Vector3 location = new Vector3(islandParts[islandParts.Count - 1].transform.position.x + size.x / 2 + islandParts[islandParts.Count - 1].transform.localScale.x / 2, 0, islandParts[islandParts.Count - 1].transform.position.z);
        //    islandParts.Add(Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
        //    islandParts[islandParts.Count - 1].transform.localScale = size;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
