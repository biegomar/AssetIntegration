using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform destination;
    [SerializeField] private Animator animator;
    
    private static readonly int isHunting = Animator.StringToHash("isHunting");
    private bool AmIHunting;

    private void Start()
    {
        this.AmIHunting = true;

        
        //gameObject.transform.rotation = Quaternion.LookRotation(myDestination);
        this.animator.SetBool(isHunting, true);
    }

    private void Update()
    {
        agent.SetDestination(destination.position);
    }
}
