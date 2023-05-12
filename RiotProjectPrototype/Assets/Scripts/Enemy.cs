using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    private NavMeshAgent agent;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }
}
