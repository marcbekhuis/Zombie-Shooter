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
    private GameObject asphalt;

    [SerializeField]
    private GameObject building;

    [SerializeField]
    private int buildingMaxFloors = 3;

    [SerializeField]
    private Vector2Int minMaxIslandPartSize = new Vector2Int(5,12);

    [SerializeField]
    private Material grassMaterial;

    [SerializeField]
    private Material asphaltMaterial;

    private bool navMeshRebuild = false;

    // Start is called before the first frame update
    void Start()
    {
        List<MeshFilter> meshFiltersGrass = new List<MeshFilter>();
        List<MeshFilter> meshFiltersAsphalt = new List<MeshFilter>();
        Vector2Int islandSize = new Vector2Int(Random.Range(5,13), Random.Range(5, 13));
        Debug.Log(islandSize);
        GameObject[,] islandParts = new GameObject[islandSize.x,islandSize.y];
        for (int x = 0; x < islandSize.x ; x++)
        {
            for (int y = 0; y < islandSize.y; y++)
            {
                if (x == 0 && y == 0)
                {
                    Vector3 size = new Vector3(Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5, 1, Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5);
                    Vector3 location = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                    if (Random.Range(0, 100) < 15)
                    {
                        islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                    }
                    else
                    {
                        islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                        if (Random.Range(0, 100) < 95)
                        {
                            PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                        }
                    }
                }
                else if (x != 0 && y == 0)
                {
                    Vector3 size = new Vector3(Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5, 1, Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5);
                    Vector3 location = new Vector3(islandParts[x - 1, y].transform.localPosition.x + size.x / 2 + islandParts[x - 1, y].transform.localScale.x / 2, 0, islandParts[x - 1, y].transform.localPosition.z - (((float)size.z / 2f) - (islandParts[x - 1, y].transform.localScale.z / 2f)));
                    if (Random.Range(0, 100) < 15)
                    {
                        islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                    }
                    else
                    {
                        islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                        if (Random.Range(0, 100) < 95)
                        {
                            PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                        }
                    }
                }
                else if (x == 0 && y != 0)
                {

                    Vector3 size = new Vector3(Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5, 1, Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5);
                    Vector3 location = new Vector3(islandParts[x, y - 1].transform.localPosition.x - (((float)size.x / 2f) - (islandParts[x, y - 1].transform.localScale.x / 2f)), 0, islandParts[x, y - 1].transform.localPosition.z + size.z / 2 + islandParts[x, y - 1].transform.localScale.z / 2);
                    if (Random.Range(0, 100) < 15)
                    {
                        islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                    }
                    else
                    {
                        islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                        if (Random.Range(0, 100) < 95)
                        {
                            PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                        }
                    }

                }
                else if (x  < islandSize.x - 1 && y < islandSize.y - 1) 
                {
                    Vector3 size = new Vector3(islandParts[x, y - 1].transform.localScale.x, 1, islandParts[x - 1,y].transform.localScale.z);
                    Vector3 location = new Vector3(islandParts[x, y - 1].transform.localPosition.x, 0, islandParts[x - 1, y].transform.localPosition.z);
                    if (Random.Range(0, 100) < 15)
                    {
                        islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                    }
                    else
                    {
                        islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                        if (Random.Range(0, 100) < 95)
                        {
                            PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                        }
                    }
                }
                else if(x == islandSize.x - 1)
                {
                    Vector3 size = new Vector3(Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5, 1, islandParts[x - 1, y].transform.localScale.z);
                    Vector3 location = new Vector3(islandParts[x - 1, y].transform.localPosition.x + islandParts[x - 1, y].transform.localScale.x / 2 + size.x /2, 0, islandParts[x - 1, y].transform.localPosition.z);
                    if (Random.Range(0, 100) < 15)
                    {
                        islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                    }
                    else
                    {
                        islandParts[x, y] = (Instantiate(asphalt, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersAsphalt.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                        if (Random.Range(0, 100) < 95)
                        {
                            PlaceBuilding(x, y, location, new Vector2Int((int)(size.x / 5), (int)(size.z / 5)));
                        }
                    }
                }
                else if (y == islandSize.y - 1)
                {
                    Vector3 size = new Vector3(islandParts[x, y - 1].transform.localScale.x, 1, Random.Range(minMaxIslandPartSize.x, minMaxIslandPartSize.y) * 5);
                    Vector3 location = new Vector3(islandParts[x, y - 1].transform.localPosition.x, 0, islandParts[x, y - 1].transform.localPosition.z + islandParts[x, y - 1].transform.localScale.z / 2 + size.z / 2);
                    if (Random.Range(0, 100) < 15)
                    {
                        islandParts[x, y] = (Instantiate(grass, location, new Quaternion(0, 0, 0, 0), this.transform));
                        meshFiltersGrass.Add(islandParts[x, y].GetComponent<MeshFilter>());
                        islandParts[x, y].transform.localScale = size;
                    }
                    else
                    {
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
        }
        {
            // Combines all the Grass meshes.
            CombineInstance[] buildingGrass = new CombineInstance[meshFiltersGrass.Count];
            for (int x = 0; x < meshFiltersGrass.Count; x++)
            {
                buildingGrass[x].mesh = meshFiltersGrass[x].sharedMesh;
                buildingGrass[x].transform = meshFiltersGrass[x].transform.localToWorldMatrix;
                Destroy(meshFiltersGrass[x].GetComponent<MeshRenderer>());
                Destroy(meshFiltersGrass[x]);
            }
            var go = new GameObject("CombinedMesh Grass");
            go.transform.SetParent(transform);
            go.AddComponent<MeshFilter>().mesh.CombineMeshes(buildingGrass);
            go.AddComponent<MeshRenderer>().sharedMaterial = grassMaterial;
            go.gameObject.layer = 8;
        }
        {
            // Combines all the Asphalt meshes.
            CombineInstance[] buildingAsphalt = new CombineInstance[meshFiltersAsphalt.Count];
            for (int x = 0; x < meshFiltersAsphalt.Count; x++)
            {
                buildingAsphalt[x].mesh = meshFiltersAsphalt[x].sharedMesh;
                buildingAsphalt[x].transform = meshFiltersAsphalt[x].transform.localToWorldMatrix;
                Destroy(meshFiltersAsphalt[x].GetComponent<MeshRenderer>());
                Destroy(meshFiltersAsphalt[x]);
            }
            var go = new GameObject("CombinedMesh Asphalt");
            go.transform.SetParent(transform);
            go.AddComponent<MeshFilter>().mesh.CombineMeshes(buildingAsphalt);
            go.AddComponent<MeshRenderer>().sharedMaterial = asphaltMaterial;
            go.gameObject.layer = 8;
        }

        //rebuildNavMesh.transform.position = islandParts[islandSize.x / 2, islandSize.y / 2].transform.position;
        //rebuildNavMesh.ResizeNavMesh(new Vector3(islandParts[0, islandSize.y / 2].transform.position.x + islandParts[islandSize.x - 1, islandSize.y / 2].transform.position.x + 100, 110, islandParts[islandSize.x / 2, 0].transform.position.x + islandParts[islandSize.x / 2, islandSize.y - 1].transform.position.x + 100));
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
