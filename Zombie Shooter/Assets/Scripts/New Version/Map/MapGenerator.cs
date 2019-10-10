using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private RebuildNavMesh[] rebuildNavMeshs;

    [SerializeField]
    private GameObject grass;

    [SerializeField]
    private GameObject asphalt;

    [SerializeField]
    private GameObject building;

    [SerializeField]
    private GameObject pushAbleCube;

    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private int buildingMaxFloors = 3;

    [SerializeField]
    private Vector2Int minMaxIslandPartSize = new Vector2Int(5, 12);

    [SerializeField]
    private Material grassMaterial;

    [SerializeField]
    private Material asphaltMaterial;

    private bool navMeshRebuild = false;
    private bool helipadSet = false;

    // Start is called before the first frame update
    void Start()
    {
        // Generates the island
        List<MeshFilter> meshFiltersGrass = new List<MeshFilter>();
        List<MeshFilter> meshFiltersAsphalt = new List<MeshFilter>();
        Vector2Int islandSize = new Vector2Int(Random.Range(5, 13), Random.Range(5, 13));
        GameObject[,] islandParts = new GameObject[islandSize.x, islandSize.y];
        for (int x = 0; x < islandSize.x; x++)
        {
            for (int y = 0; y < islandSize.y; y++)
            {
                // Spawns the first tile
                if (x == 0 && y == 0)
                {
                    CreateFirstTile(x, y, meshFiltersGrass, meshFiltersAsphalt, islandParts);
                }
                // Spawns the the tiles on Y as number 0
                else if (x != 0 && y == 0)
                {
                    CreateY0Tiles(x, y, meshFiltersGrass, meshFiltersAsphalt, islandParts);
                }
                // Spawns the the tiles on X as number 0
                else if (x == 0 && y != 0)
                {
                    CreateX0Tiles(x, y, meshFiltersGrass, meshFiltersAsphalt, islandParts);
                }
                // Spawns all the tiles in the middle
                else if (x < islandSize.x - 1 && y < islandSize.y - 1)
                {
                    CreateInsideTiles(x, y, meshFiltersGrass, meshFiltersAsphalt, islandParts);
                }
                // Spawns the last tiles on the X as.
                else if (x == islandSize.x - 1)
                {
                    CreateXLastTiles(x, y, meshFiltersGrass, meshFiltersAsphalt, islandParts);
                }
                // spawns the last tiles on the Y as.
                else if (y == islandSize.y - 1)
                {
                    CreateYLastTiles(x, y, meshFiltersGrass, meshFiltersAsphalt, islandParts);
                }
            }
            CombineMesh(meshFiltersGrass, grassMaterial, "CombinedMesh Grass");
            CombineMesh(meshFiltersAsphalt, asphaltMaterial, "CombinedMesh Asphalt");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!navMeshRebuild)
        {
            foreach (var rebuildNavMesh in rebuildNavMeshs)
            {
                rebuildNavMesh.Rebuild();
            }
            navMeshRebuild = true;
        }
    }

    // Spawns a random generated building.
    void PlaceBuilding(int x, int y, Vector3 position, Vector2Int size)
    {
        GameObject justPlaced = Instantiate(building, position - new Vector3(size.x / 2f * 5 - 7.5f, 0, size.y / 2f * 5 - 7.5f), new Quaternion(0, 0, 0, 0));
        BuildBuilder buildBuilder = justPlaced.GetComponent<BuildBuilder>();
        justPlaced.name = "Building X" + x + "Y" + y;
        buildBuilder.size = size - new Vector2Int(2, 2);
        buildBuilder.maxFloors = buildingMaxFloors;
        if (!helipadSet && ((size.x > 2 && size.y > 3) || (size.x > 3 && size.y > 2)))
        {
            buildBuilder.hasHelipad = true;
            buildBuilder.winScreen = winScreen;
            helipadSet = true;
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

    void CreateFirstTile(int x, int y, List<MeshFilter> meshFiltersGrass, List<MeshFilter> meshFiltersAsphalt, GameObject[,] islandParts)
    {
        Vector3 size = new Vector3(Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5, 1, Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5);
        Vector3 location = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        // Spawns a grass tile
        if (Random.Range(0, 100) < 15)
        {
            islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 20)
            {
                GameObject justSpawned = (Instantiate(pushAbleCube, location + new Vector3(Random.Range(-10, 10), 7, Random.Range(-10, 10)), new Quaternion(0, 0, 0, 0), this.transform));
                justSpawned.transform.localScale = new Vector3(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));
            }
        }
        else
        {
            // Spawns a Asphalt tile where a building can be spawned on.
            islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 95)
            {
                PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
            }
        }
    }

    void CreateY0Tiles(int x, int y, List<MeshFilter> meshFiltersGrass, List<MeshFilter> meshFiltersAsphalt, GameObject[,] islandParts)
    {
        Vector3 size = new Vector3(Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5, 1, Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5);
        Vector3 location = new Vector3(islandParts[x - 1, y].transform.localPosition.x + size.x / 2 + islandParts[x - 1, y].transform.localScale.x / 2, 0, islandParts[x - 1, y].transform.localPosition.z - (((float)size.z / 2f) - (islandParts[x - 1, y].transform.localScale.z / 2f)));
        // Spawns a grass tile
        if (Random.Range(0, 100) < 15)
        {
            islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 20)
            {
                GameObject justSpawned = (Instantiate(pushAbleCube, location + new Vector3(Random.Range(-10, 10), 7, Random.Range(-10, 10)), new Quaternion(0, 0, 0, 0), this.transform));
                justSpawned.transform.localScale = new Vector3(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));
            }
        }
        else
        {
            // Spawns a Asphalt tile where a building can be spawned on.
            islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 95)
            {
                PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
            }
        }
    }

    void CreateX0Tiles(int x, int y, List<MeshFilter> meshFiltersGrass, List<MeshFilter> meshFiltersAsphalt, GameObject[,] islandParts)
    {
        Vector3 size = new Vector3(Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5, 1, Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5);
        Vector3 location = new Vector3(islandParts[x, y - 1].transform.localPosition.x - (((float)size.x / 2f) - (islandParts[x, y - 1].transform.localScale.x / 2f)), 0, islandParts[x, y - 1].transform.localPosition.z + size.z / 2 + islandParts[x, y - 1].transform.localScale.z / 2);
        // Spawns a grass tile
        if (Random.Range(0, 100) < 15)
        {
            islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 20)
            {
                GameObject justSpawned = (Instantiate(pushAbleCube, location + new Vector3(Random.Range(-10, 10), 7, Random.Range(-10, 10)), new Quaternion(0, 0, 0, 0), this.transform));
                justSpawned.transform.localScale = new Vector3(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));
            }
        }
        else
        {
            // Spawns a Asphalt tile where a building can be spawned on.
            islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 95)
            {
                PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
            }
        }
    }

    void CreateInsideTiles(int x, int y, List<MeshFilter> meshFiltersGrass, List<MeshFilter> meshFiltersAsphalt, GameObject[,] islandParts)
    {
        Vector3 size = new Vector3(islandParts[x, y - 1].transform.localScale.x, 1, islandParts[x - 1, y].transform.localScale.z);
        Vector3 location = new Vector3(islandParts[x, y - 1].transform.localPosition.x, 0, islandParts[x - 1, y].transform.localPosition.z);
        // Spawns a grass tile
        if (Random.Range(0, 100) < 15)
        {
            islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 20)
            {
                GameObject justSpawned = (Instantiate(pushAbleCube, location + new Vector3(Random.Range(-10, 10), 7, Random.Range(-10, 10)), new Quaternion(0, 0, 0, 0), this.transform));
                justSpawned.transform.localScale = new Vector3(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));
            }
        }
        else
        {
            // Spawns a Asphalt tile where a building can be spawned on.
            islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 95)
            {
                PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
            }
        }
    }

    void CreateXLastTiles(int x, int y, List<MeshFilter> meshFiltersGrass, List<MeshFilter> meshFiltersAsphalt, GameObject[,] islandParts)
    {
        Vector3 size = new Vector3(Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5, 1, islandParts[x - 1, y].transform.localScale.z);
        Vector3 location = new Vector3(islandParts[x - 1, y].transform.localPosition.x + islandParts[x - 1, y].transform.localScale.x / 2 + size.x / 2, 0, islandParts[x - 1, y].transform.localPosition.z);
        // Spawns a grass tile
        if (Random.Range(0, 100) < 15)
        {
            islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 20)
            {
                GameObject justSpawned = (Instantiate(pushAbleCube, location + new Vector3(Random.Range(-10, 10), 7, Random.Range(-10, 10)), new Quaternion(0, 0, 0, 0), this.transform));
                justSpawned.transform.localScale = new Vector3(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));
            }
        }
        else
        {
            // Spawns a Asphalt tile where a building can be spawned on.
            islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 95)
            {
                PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
            }
        }
    }

    void CreateYLastTiles(int x, int y, List<MeshFilter> meshFiltersGrass, List<MeshFilter> meshFiltersAsphalt, GameObject[,] islandParts)
    {
        Vector3 size = new Vector3(islandParts[x, y - 1].transform.localScale.x, 1, Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5);
        Vector3 location = new Vector3(islandParts[x, y - 1].transform.localPosition.x, 0, islandParts[x, y - 1].transform.localPosition.z + islandParts[x, y - 1].transform.localScale.z / 2 + size.z / 2);
        // Spawns a grass tile
        if (Random.Range(0, 100) < 15)
        {
            islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 20)
            {
                GameObject justSpawned = (Instantiate(pushAbleCube, location + new Vector3(Random.Range(-10, 10), 7, Random.Range(-10, 10)), new Quaternion(0, 0, 0, 0), this.transform));
                justSpawned.transform.localScale = new Vector3(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));
            }
        }
        else
        {
            // Spawns a Asphalt tile where a building can be spawned on.
            islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
            meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
            islandParts[x, y].transform.localScale = size;
            if (Random.Range(0, 100) < 95)
            {
                PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
            }
        }
    }
}
