using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuilder : MonoBehaviour
{
    public int maxFloors = 3;

    public Vector2Int size = new Vector2Int (2,2);

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

    // Start is called before the first frame update
    void Start()
    {
        // Generates a random building
        List<MeshFilter> meshFiltersConcrete = new List<MeshFilter>();
        List<MeshFilter> meshFiltersFloor = new List<MeshFilter>();
        int floors = Random.Range(2, maxFloors);
        // Loops for every floor
        for (int currentFloor = 0; currentFloor < floors; currentFloor++)
        {
            GameObject currentFloorObject = new GameObject("Floor " + currentFloor.ToString());
            currentFloorObject.transform.parent = this.transform;
            currentFloorObject.transform.position = new Vector3(this.transform.position.x, 5.75f * currentFloor, this.transform.position.z);
            int maxDoors = Mathf.Clamp((size.x * 2 + size.y * 2) / 8, 1, 10);
            int doorsPlaced = 0;

            for (int x = 0; x < size.x; x++)
            {

                for (int y = 0; y < size.y; y++)
                {
                    // Spawn a floor and ceiling
                    GameObject justPlaced = Instantiate(floor, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y + 0.75f, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                    meshFiltersFloor.Add(justPlaced.GetComponent<MeshFilter>());
                    justPlaced = Instantiate(ceiling, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y + 6f, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                    meshFiltersConcrete.Add(justPlaced.GetComponent<MeshFilter>());

                    // Spawns a wall on the Y as number 0
                    if (y == 0)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 60) < 10)
                        {
                            justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y - 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            doorsPlaced++;
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFiltersConcrete.Add(meshFilter);
                            }
                        }
                        else
                        {
                            justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y - 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFiltersConcrete.Add(meshFilter);
                            }
                        }
                    }
                    // Spawns a wall on the last y as.
                    else if (y == size.y - 1)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 60) < 10)
                        {
                            justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y + 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            doorsPlaced++;
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFiltersConcrete.Add(meshFilter);
                            }
                        }
                        else
                        {
                            justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y + 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFiltersConcrete.Add(meshFilter);
                            }
                        }
                    }
                    // Spawns a wall on the X as number 0
                    if (x == 0)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 60) < 10)
                        {
                            justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x - 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            doorsPlaced++;
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFiltersConcrete.Add(meshFilter);
                            }
                        }
                        else
                        {
                            justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x - 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFiltersConcrete.Add(meshFilter);
                            }
                        }
                    }
                    // Spawns a wall on the last X as.
                    else if (x == size.x - 1)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 60) < 10)
                        {
                            justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x + 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            doorsPlaced++;
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFiltersConcrete.Add(meshFilter);
                            }
                        }
                        else
                        {
                            justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x + 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFiltersConcrete.Add(meshFilter);
                            }
                        }
                    }
                }
            }
        }
        {
            // Combines all the Floor meshes
            CombineInstance[] buildingFloor = new CombineInstance[meshFiltersFloor.Count];
            for (int x = 0; x < meshFiltersFloor.Count; x++)
            {
                buildingFloor[x].mesh = meshFiltersFloor[x].sharedMesh;
                buildingFloor[x].transform = meshFiltersFloor[x].transform.localToWorldMatrix;
                Destroy(meshFiltersFloor[x].GetComponent<MeshRenderer>());
                Destroy(meshFiltersFloor[x]);
            }
            var go = new GameObject("CombinedMesh Floor");
            go.transform.SetParent(transform);
            go.AddComponent<MeshFilter>().mesh.CombineMeshes(buildingFloor);
            go.AddComponent<MeshRenderer>().sharedMaterial = floorMaterial;
            go.gameObject.layer = 8;
        }
        {
            // Combines all the concrete meshes
            CombineInstance[] buildingConcrete = new CombineInstance[meshFiltersConcrete.Count];
            for (int x = 0; x < meshFiltersConcrete.Count; x++)
            {
                buildingConcrete[x].mesh = meshFiltersConcrete[x].sharedMesh;
                buildingConcrete[x].transform = meshFiltersConcrete[x].transform.localToWorldMatrix;
                Destroy(meshFiltersConcrete[x].GetComponent<MeshRenderer>());
                Destroy(meshFiltersConcrete[x]);
            }
            var go = new GameObject("CombinedMesh Concrete");
            go.transform.SetParent(transform);
            go.AddComponent<MeshFilter>().mesh.CombineMeshes(buildingConcrete);
            go.AddComponent<MeshRenderer>().sharedMaterial = wallMaterial;
            go.gameObject.layer = 8;
        }
    }
}
