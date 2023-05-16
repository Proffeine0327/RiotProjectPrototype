using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace first
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float atkTime;
        [SerializeField] private float hp;

        private Transform target;
        private Shutter shutter;
        private float curAtkTime;
        private float curHp;

        private NavMeshAgent agent;

        public float CurHp => curHp;
        public float Hp => hp;

        public void Damage(float amount) => curHp -= amount;
        public void Init(Shutter sht) => shutter = sht;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            curHp = hp;
        }

        private void Update()
        {
            if (curHp <= 0)
            {
                Player.player.AddKillAmount();
                Destroy(gameObject);
                return;
            }

            SetTarget();
        }

        private void SetTarget()
        {
            if (shutter == null) return;

            if (!shutter.locked) target = Player.player.transform;
            else
            {
                target = shutter.transform;
                if (Vector3.Distance(transform.position, target.position) < 2)
                {
                    if (curAtkTime <= 0)
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
}