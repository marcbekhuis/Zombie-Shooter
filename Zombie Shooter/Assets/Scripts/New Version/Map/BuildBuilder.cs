using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuilder : MonoBehaviour
{
    public int maxFloors = 3;

    public Vector2Int size = new Vector2Int (2,2);

    public bool hasHelipad = false;

    [SerializeField]
    private GameObject floor;

    [SerializeField]
    private GameObject ceiling;

    [SerializeField]
    private GameObject doorWay;

    [SerializeField]
    private GameObject window;

    [SerializeField]
    private Material wallMaterial;

    [SerializeField]
    private Material floorMaterial;

    [SerializeField]
    private GameObject helipad;

    [SerializeField]
    private GameObject helicopter;

    [SerializeField]
    private GameObject stair;

    private bool helipadPlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        // Generates a random building
        List<MeshFilter> meshFiltersConcrete = new List<MeshFilter>();
        List<MeshFilter> meshFiltersFloor = new List<MeshFilter>();
        int floors = Random.Range(2, maxFloors);
        Vector2Int[,] stairsLocations = new Vector2Int[2,2];

        // Loops for every floor
        for (int currentFloor = 0; currentFloor < floors; currentFloor++)
        {
            GameObject currentFloorObject = new GameObject("Floor " + currentFloor.ToString());
            currentFloorObject.transform.parent = this.transform;
            currentFloorObject.transform.position = new Vector3(this.transform.position.x, 5.75f * currentFloor, this.transform.position.z);
            int maxDoors = Mathf.Clamp((size.x * 2 + size.y * 2) / 8, 1, 10);
            int doorsPlaced = 0;

            SpawnHelipad(floors,currentFloor,currentFloorObject);
            SpawnStair(stairsLocations, currentFloorObject);

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    // Spawn a floor and ceiling
                    if (currentFloor == 0)
                    {
                        GameObject spawnedFloor = Instantiate(floor, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y + 0.75f, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                        meshFiltersFloor.Add(spawnedFloor.GetComponent<MeshFilter>());
                    }
                    else if (stairsLocations[1, 0] == new Vector2Int(x, y) || stairsLocations[1, 1] == new Vector2Int(x, y))
                    {
                    }
                    else
                    {
                        GameObject spawnedFloor = Instantiate(floor, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y + 0.75f, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                        meshFiltersFloor.Add(spawnedFloor.GetComponent<MeshFilter>());
                    }
                    if (stairsLocations[0, 0] == new Vector2Int(x, y) || stairsLocations[0, 1] == new Vector2Int(x, y))
                    {
                    }
                    else
                    {
                        GameObject justPlaced = Instantiate(ceiling, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y + 6f, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                        meshFiltersConcrete.Add(justPlaced.GetComponent<MeshFilter>());
                    }

                    // Spawns a wall on the Y as number 0
                    if (y == 0)
                    {
                        CreateWall(currentFloor,doorsPlaced,maxDoors, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y - 2.75f), new Vector3(0, 90, 0), currentFloorObject.transform,meshFiltersConcrete);
                    }
                    // Spawns a wall on the last y as.
                    else if (y == size.y - 1)
                    {
                        CreateWall(currentFloor, doorsPlaced, maxDoors, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y + 2.75f), new Vector3(0, -90, 0), currentFloorObject.transform, meshFiltersConcrete);
                    }
                    // Spawns a wall on the X as number 0
                    if (x == 0)
                    {
                        CreateWall(currentFloor, doorsPlaced, maxDoors, new Vector3(currentFloorObject.transform.position.x + 5 * x - 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Vector3(0, 180, 0), currentFloorObject.transform, meshFiltersConcrete);
                    }
                    // Spawns a wall on the last X as.
                    else if (x == size.x - 1)
                    {
                        CreateWall(currentFloor, doorsPlaced, maxDoors, new Vector3(currentFloorObject.transform.position.x + 5 * x + 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Vector3(0, 0, 0), currentFloorObject.transform, meshFiltersConcrete);
                    }
                }
            }
            stairsLocations[1, 0] = stairsLocations[0, 0];
            stairsLocations[1, 1] = stairsLocations[0, 1];
        }
        CombineMesh(meshFiltersFloor,floorMaterial, "CombinedMesh Floor");
        CombineMesh(meshFiltersConcrete, wallMaterial, "CombinedMesh Concrete");
    }

    void CreateWall(int currentFloor, int doorsPlaced, int maxDoors, Vector3 location, Vector3 rotation ,Transform currentFloorObject, List<MeshFilter> meshFiltersConcrete)
    {
        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 60) < 10)
        {
            GameObject justPlaced = Instantiate(doorWay, location, new Quaternion(0, 0, 0, 0), currentFloorObject);
            justPlaced.transform.eulerAngles = rotation;
            doorsPlaced++;
            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
            {
                meshFiltersConcrete.Add(meshFilter);
            }
        }
        else
        {
            GameObject justPlaced = Instantiate(window, location, new Quaternion(0, 0, 0, 0), currentFloorObject);
            justPlaced.transform.eulerAngles = rotation;
            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
            {
                meshFiltersConcrete.Add(meshFilter);
            }
        }
    }
    void CombineMesh(List<MeshFilter> meshFilters, Material material, string name)
    {
        // Combines all the Grass meshes.
        CombineInstance[] building = new CombineInstance[meshFilters.Count];
        for (int x = 0; x < meshFilters.Count; x++)
        {
            building[x].mesh = meshFilters[x].sharedMesh;
            building[x].transform = meshFilters[x].transform.localToWorldMatrix;
            Destroy(meshFilters[x].GetComponent<MeshRenderer>());
            Destroy(meshFilters[x]);
        }
        var go = new GameObject(name);
        go.transform.SetParent(transform);
        go.AddComponent<MeshFilter>().mesh.CombineMeshes(building);
        go.AddComponent<MeshRenderer>().sharedMaterial = material;
        go.gameObject.layer = 8;
    }

    void SpawnHelipad(int floors, int currentFloor, GameObject currentFloorObject)
    {
        if (hasHelipad && currentFloor == floors - 1 && !helipadPlaced)
        {
            GameObject spawnedHelipad = Instantiate(helipad, new Vector3(currentFloorObject.transform.position.x + 5 * Random.Range(1, size.x - 1), currentFloorObject.transform.position.y + 6.5f, currentFloorObject.transform.position.z + 5 * Random.Range(1, size.y - 1)), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
            GameObject spawnedHelicopter = Instantiate(helicopter, new Vector3(Random.Range(700, 1000), 120, Random.Range(700, 1000)), new Quaternion(0, 0, 0, 0));
            spawnedHelicopter.GetComponent<Helicopter>().landingZone = spawnedHelipad.transform;
            helipadPlaced = true;
        }
    }

    void SpawnStair(Vector2Int[,] stairsLocations, GameObject currentFloorObject)
    {
        stairsLocations[0, 0] = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
        bool setSecondStairLoc = false;
        int safety = 0;
        do
        {
            int randomNumber = Random.Range(0, 100);
            if (randomNumber < 25 && stairsLocations[0, 0].y < size.y - 2)
            {
                stairsLocations[0, 1] = stairsLocations[0, 0] + new Vector2Int(0, 1);
                setSecondStairLoc = true;
            }
            else if (randomNumber < 50 && stairsLocations[0, 0].x < size.x - 2)
            {
                stairsLocations[0, 1] = stairsLocations[0, 0] + new Vector2Int(1, 0);
                setSecondStairLoc = true;
            }
            else if (randomNumber < 50 && stairsLocations[0, 0].y > 1)
            {
                stairsLocations[0, 1] = stairsLocations[0, 0] - new Vector2Int(0, 1);
                setSecondStairLoc = true;
            }
            else if (stairsLocations[0, 0].x > 1)
            {
                stairsLocations[0, 1] = stairsLocations[0, 0] - new Vector2Int(1, 0);
                setSecondStairLoc = true;
            }
            safety++;
        } while (!setSecondStairLoc && safety < 150);

        if (stairsLocations[0, 0].x + 1 == stairsLocations[0, 1].x)
        {
            GameObject spawnedStair = Instantiate(stair, new Vector3(currentFloorObject.transform.position.x + stairsLocations[0, 0].x * 5 + 2.5f, currentFloorObject.transform.position.y + 0, currentFloorObject.transform.position.z + stairsLocations[0, 0].y * 5), new Quaternion(0, 0, 0, 0));
            spawnedStair.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (stairsLocations[0, 0].x - 1 == stairsLocations[0, 1].x)
        {
            GameObject spawnedStair = Instantiate(stair, new Vector3(currentFloorObject.transform.position.x + stairsLocations[0, 0].x * 5 - 2.5f, currentFloorObject.transform.position.y + 0, currentFloorObject.transform.position.z + stairsLocations[0, 0].y * 5), new Quaternion(0, 0, 0, 0));
            spawnedStair.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (stairsLocations[0, 0].y + 1 == stairsLocations[0, 1].y)
        {
            GameObject spawnedStair = Instantiate(stair, new Vector3(currentFloorObject.transform.position.x + stairsLocations[0, 0].x * 5, currentFloorObject.transform.position.y + 0, currentFloorObject.transform.position.z + stairsLocations[0, 0].y * 5 + 2.5f), new Quaternion(0, 0, 0, 0));
            spawnedStair.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            GameObject spawnedStair = Instantiate(stair, new Vector3(currentFloorObject.transform.position.x + stairsLocations[0, 0].x * 5, currentFloorObject.transform.position.y + 0, currentFloorObject.transform.position.z + stairsLocations[0, 0].y * 5 - 2.5f), new Quaternion(0, 0, 0, 0));
            spawnedStair.transform.eulerAngles = new Vector3(0, -90, 0);
        }
    }
}
