
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace third
{

    public class Enemy : Unit
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackTime;
        [SerializeField] private GameObject bloodParticle;

        private float curAttackTime;
        private PlayerUnit targetUnit;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            if (curHp <= 0)
            {
                Instantiate(bloodParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
                return;
            }

            if (curAttackTime > 0) curAttackTime -= Time.deltaTime;
            if (targetUnit != null)
            {
                if (Vector3.Distance(transform.position, targetUnit.transform.position) <= attackRange)
                {
                    if (curAttackTime <= 0)
                    {
                        targetUnit.Damage(20);

                        curAttackTime = attackTime;
                    }
                }
            }
            else
            {
                var enemys = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Unit"));
                if (enemys.Length != 0) targetUnit = enemys[0].GetComponent<PlayerUnit>();
            }
        }
    }
}