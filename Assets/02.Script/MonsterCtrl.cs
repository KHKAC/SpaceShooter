using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    Transform monsterTr;
    Transform playerTr;
    NavMeshAgent agent;

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        // agent.destination = playerTr.position;
        agent.SetDestination(playerTr.position);
    }
}
