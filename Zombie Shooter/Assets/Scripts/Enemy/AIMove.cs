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
        // Makes the AI move to the player or to it self when the player lost or paused the game.
        if (!PlayerHealth.gameOver && !PauseMenuToggle.gamePaused)
        {
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            navMeshAgent.SetDestination(this.transform.position);
        }
    }
}
