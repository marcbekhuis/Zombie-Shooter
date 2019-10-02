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

    CombineInstance[] building;

    // Start is called before the first frame update
    void Start()
    {
        List<MeshFilter> meshFilters = new List<MeshFilter>();
        int floors = Random.Range(2,maxFloors);
        for (int currentFloor = 0; currentFloor < floors; currentFloor++)
        {
            GameObject currentFloorObject = Instantiate(new GameObject(), new Vector3(this.transform.position.x, 5.75f * currentFloor, this.transform.position.z), new Quaternion(0,0,0,0), this.transform);
            currentFloorObject.name = "Floor " + currentFloor.ToString();
            int maxDoors = Mathf.Clamp((size.x * 2 + size.y * 2) / 10, 1, 10);
            int doorsPlaced = 0;

            for (int x = 0; x < size.x; x++)
            {

                for (int y = 0; y < size.y; y++)
                {
                    Instantiate(floor, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y + 0.75f, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                    GameObject justPlaced = Instantiate(ceiling, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y + 6f, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                    meshFilters.Add(justPlaced.GetComponent<MeshFilter>());

                    if (y == 0)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 30) < 10)
                        {
                            justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y - 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            doorsPlaced++;
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFilters.Add(meshFilter);
                            }
                        }
                        else
                        {
                            justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y - 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFilters.Add(meshFilter);
                            }
                        }
                    }
                    else if (y == size.y - 1)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 30) < 10)
                        {
                            justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y + 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            doorsPlaced++;
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFilters.Add(meshFilter);
                            }
                        }
                        else
                        {
                            justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y + 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFilters.Add(meshFilter);
                            }
                        }
                    }
                    if (x == 0)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 30) < 10)
                        {
                            justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x - 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            doorsPlaced++;
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFilters.Add(meshFilter);
                            }
                        }
                        else
                        {
                            justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x - 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFilters.Add(meshFilter);
                            }
                        }
                    }
                    else if (x == size.x - 1)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 30) < 10)
                        {
                            justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x + 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            doorsPlaced++;
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFilters.Add(meshFilter);
                            }
                        }
                        else
                        {
                            justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x + 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            foreach (var meshFilter in justPlaced.GetComponentsInChildren<MeshFilter>())
                            {
                                meshFilters.Add(meshFilter);
                            }
                        }
                    }
                }
            }
        }
        building = new CombineInstance[meshFilters.Count];
        for (int x = 0; x < meshFilters.Count; x++)
        {
            building[x].mesh = meshFilters[x].sharedMesh;
            building[x].transform = meshFilters[x].transform.localToWorldMatrix;
            Destroy(meshFilters[x].GetComponent<MeshRenderer>());
            Destroy(meshFilters[x]);
        }
        var go = new GameObject("CombinedMesh");
        go.transform.SetParent(transform);
        go.AddComponent<MeshFilter>().mesh.CombineMeshes(building);
        go.AddComponent<MeshRenderer>().sharedMaterial = wallMaterial;
    }
}
