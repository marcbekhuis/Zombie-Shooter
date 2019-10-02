using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private RebuildNavMesh rebuildNavMesh;

    [SerializeField]
    private GameObject grass;

    [SerializeField]
    private GameObject building;

    [SerializeField]
    private int buildingMaxFloors = 3;

    private bool navMeshRebuild = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector2Int islandSize = new Vector2Int(Random.Range(3,13), Random.Range(3, 13));
        Debug.Log(islandSize);
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
                    PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                }
                else if (x != 0 && y == 0)
                {
                    Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, Random.Range(4, 8) * 5);
                    Vector3 location = new Vector3(islandParts[x - 1, y].transform.localPosition.x + size.x / 2 + islandParts[x - 1, y].transform.localScale.x / 2, 0, islandParts[x - 1, y].transform.localPosition.z - (((float)size.z / 2f) - (islandParts[x - 1, y].transform.localScale.z / 2f)));
                    islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                    islandParts[x, y].transform.localScale = size;
                    PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                }
                else if (x == 0 && y != 0)
                {
                    Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, Random.Range(4, 8) * 5);
                    Vector3 location = new Vector3(islandParts[x, y - 1].transform.localPosition.x - (((float)size.x / 2f) - (islandParts[x, y - 1].transform.localScale.x / 2f)), 0, islandParts[x, y - 1].transform.localPosition.z + size.z / 2 + islandParts[x, y - 1].transform.localScale.z / 2);
                    islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                    islandParts[x, y].transform.localScale = size;
                    PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                }
                else if (x  < islandSize.x - 1 && y < islandSize.y - 1) 
                {
                    Vector3 size = new Vector3(islandParts[x, y - 1].transform.localScale.x, 1, islandParts[x - 1,y].transform.localScale.z);
                    Vector3 location = new Vector3(islandParts[x, y - 1].transform.localPosition.x, 0, islandParts[x - 1, y].transform.localPosition.z);
                    islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                    islandParts[x, y].transform.localScale = size;
                    PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                }
                else if(x == islandSize.x - 1)
                {
                    Vector3 size = new Vector3(Random.Range(4, 8) * 5, 1, islandParts[x - 1, y].transform.localScale.z);
                    Vector3 location = new Vector3(islandParts[x - 1, y].transform.localPosition.x + islandParts[x - 1, y].transform.localScale.x / 2 + size.x /2, 0, islandParts[x - 1, y].transform.localPosition.z);
                    islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                    islandParts[x, y].transform.localScale = size;
                    PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                }
                else if (y == islandSize.y - 1)
                {
                    Vector3 size = new Vector3(islandParts[x, y - 1].transform.localScale.x, 1, Random.Range(4, 8) * 5);
                    Vector3 location = new Vector3(islandParts[x, y - 1].transform.localPosition.x, 0, islandParts[x, y - 1].transform.localPosition.z + islandParts[x, y - 1].transform.localScale.z / 2 + size.z / 2);
                    islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                    islandParts[x, y].transform.localScale = size;
                    PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!navMeshRebuild)
        {
            rebuildNavMesh.Rebuild();
            navMeshRebuild = true;
        }
    }

    void PlaceBuilding(int x, int y, Vector3 position, Vector2Int size)
    {
        GameObject justPlaced = Instantiate(building, position - new Vector3(size.x / 2f * 5 - 7.5f, 0, size.y / 2f * 5 - 7.5f), new Quaternion(0,0,0,0));
        BuildBuilder buildBuilder = justPlaced.GetComponent<BuildBuilder>();
        justPlaced.name = "Building X" + x + "Y" + y;
        buildBuilder.size = size - new Vector2Int(2,2);
        buildBuilder.maxFloors = buildingMaxFloors;
    }
}
