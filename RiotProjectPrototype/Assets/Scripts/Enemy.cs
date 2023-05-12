using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float atkTime;

    private Transform target;
    private Shutter shutter;
    private float curAtkTime;

    private NavMeshAgent agent;

    public void Init(Shutter sht) => shutter = sht;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        SetTarget();
    }

    private void SetTarget()
    {
        if(shutter == null) return;
        
        if(!shutter.locked) target = Player.player.transform;
        else
        {
            target = shutter.transform;
            if(Vector3.Distance(transform.position, target.position) < 2)
            {
                if(curAtkTime <= 0)
                {
                    shutter.Damage();
                    curAtkTime = atkTime;
                }
                else
                {
                    curAtkTime -= Time.deltaTime;
                }
            }
        }

        agent.SetDestination(target.position);
    }
}
