using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit : Unit
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTime;

    private bool isOnAttackField;
    private float curAttackTime;
    private NavMeshAgent agent;
    private Enemy targetEnemy;
    private Coroutine randomMoveCoroutine;

    public bool IsOnAttackField => isOnAttackField;

    public void Dragging(bool isDragging)
    {
        agent.enabled = !isDragging;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange - 0.1f;
    }

    protected override void Start()
    {
        base.Start();
        randomMoveCoroutine = StartCoroutine(RandomMove());
    }

    protected override void Update()
    {
        base.Update();
        CheckField();
        Attack();
    }

    private void CheckField()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, 2, LayerMask.GetMask("Ground")))
        {
            if (hit.collider.CompareTag("AttackField"))
            {
                isOnAttackField = true;
            }
            else
            {
                isOnAttackField = false;
            }
        }
    }

    private IEnumerator RandomMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 4f));
            if (agent.enabled && !isOnAttackField) agent.SetDestination(new Vector3(Random.Range(-2.5f, 2.5f), 1, Random.Range(-3.8f, -7.38f)));
        }
    }

    private void Attack()
    {
        if (!isOnAttackField) return;
        if (targetEnemy != null)
        {
            if (agent.enabled) agent.SetDestination(targetEnemy.transform.position);

            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= attackRange)
            {
                if (curAttackTime <= 0)
                {
                    targetEnemy.GetComponent<Enemy>().Damage(20);

                    curAttackTime = attackTime;
                }
            }
        }
        else
        {
            var enemys = Physics.OverlapSphere(transform.position, 30, LayerMask.GetMask("Enemy"));
            enemys = enemys.OrderBy(element => Vector3.Distance(transform.position, element.transform.position)).ToArray();

            if (enemys.Length != 0) targetEnemy = enemys[0].GetComponent<Enemy>();
        }
        if (curAttackTime > 0) curAttackTime -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
