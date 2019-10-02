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

    // Start is called before the first frame update
    void Start()
    {
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
                    Instantiate(ceiling, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y + 6f, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);

                    if (y == 0)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 30) < 10)
                        {
                            Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y - 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            doorsPlaced++;
                        }
                        else
                        {
                            Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y - 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                        }
                    }
                    else if (y == size.y - 1)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 30) < 10)
                        {
                            Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y + 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            doorsPlaced++;
                        }
                        else
                        {
                            Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y + 2.75f), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                        }
                    }
                    if (x == 0)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 30) < 10)
                        {
                            GameObject justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x - 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            doorsPlaced++;
                        }
                        else
                        {
                            GameObject justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x - 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                        }
                    }
                    else if (x == size.x - 1)
                    {
                        if (currentFloor == 0 && doorsPlaced < maxDoors && Random.Range(0, 30) < 10)
                        {
                            GameObject justPlaced = Instantiate(doorWay, new Vector3(currentFloorObject.transform.position.x + 5 * x + 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                            doorsPlaced++;
                        }
                        else
                        {
                            GameObject justPlaced = Instantiate(window, new Vector3(currentFloorObject.transform.position.x + 5 * x + 2.75f, currentFloorObject.transform.position.y, currentFloorObject.transform.position.z + 5 * y), new Quaternion(0, 0, 0, 0), currentFloorObject.transform);
                            justPlaced.transform.eulerAngles = new Vector3(0, 90, 0);
                        }
                    }
                }
            }
        }   
    }
}
