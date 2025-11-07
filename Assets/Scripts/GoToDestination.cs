using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;



public class GoToDestination : MonoBehaviour

{
    public Transform destination; 

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navMeshAgent.SetDestination(destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        animator.SetFloat("speed", speed);
    }
}

