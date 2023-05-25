using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

namespace second
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float detectRange;
        [SerializeField] private float attackRange;

        string[] targetTags;
        private Transform castleTarget;
        private Transform detectTarget;
        private Transform atkTarget;
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            agent.stoppingDistance = attackRange;
        }

        public void Init(Transform castleTarget, string[] targetTags)
        {
            this.castleTarget = castleTarget;
            this.targetTags = targetTags;
        }

        private void Update()
        {
            SetTarget();

            if (atkTarget != null) agent.enabled = false;
            else
            {
                agent.SetDestination(castleTarget.position);
                if (detectTarget != null) agent.SetDestination(detectTarget.position);
            }
        }

        private void SetTarget()
        {
            if (atkTarget != null) return;

            var detectUnits = Physics.OverlapSphere(transform.position, detectRange);
            var attackUnits = Physics.OverlapSphere(transform.position, attackRange);

            detectUnits = detectUnits.OrderBy(item => Vector3.Distance(item.transform.position, transform.position)).ToArray();
            attackUnits = attackUnits.OrderBy(item => Vector3.Distance(item.transform.position, transform.position)).ToArray();

            foreach (var unit in detectUnits)
            {
                foreach (var tag in targetTags)
                    if (unit.CompareTag(tag)) detectTarget = unit.transform;
            }

            foreach (var unit in attackUnits)
            {
                foreach (var tag in targetTags)
                    if (unit.CompareTag(tag)) atkTarget = unit.transform;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}