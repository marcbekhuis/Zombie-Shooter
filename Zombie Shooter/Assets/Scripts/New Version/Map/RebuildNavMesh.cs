﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RebuildNavMesh : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface navMeshSurface;

    // Rebuilds the NavMesh
    public void Rebuild()
    {
        navMeshSurface.BuildNavMesh();
    }

    public void ResizeNavMesh(Vector3 size)
    {
        navMeshSurface.size = size;
    }
}
