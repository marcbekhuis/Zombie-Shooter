using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RebuildNavMesh : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface navMeshSurface;

    [SerializeField]
    NavMeshData navMeshData;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            navMeshSurface.UpdateNavMesh(navMeshData);
        }
    }
}
