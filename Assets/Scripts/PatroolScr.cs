using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatroolScr : MonoBehaviour
{
    public Transform endPoint;

    public float speed;

    public NavMeshAgent agent;
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
