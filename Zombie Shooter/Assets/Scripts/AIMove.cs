using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{

    public Transform player;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        navMeshAgent.SetDestination(player.position);
    }
}
