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

    public void Rebuild()
    {
        navMeshSurface.UpdateNavMesh(navMeshData);
    }

    public void ResizeNavMesh(Vector3 size)
    {
        navMeshSurface.size = size;
    }
}
