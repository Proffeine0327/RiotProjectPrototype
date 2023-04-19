using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackTime;
    [SerializeField] private float damage;

    private float curAttackTime;

    private void Update()
    {
        var diff = Player.player.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg, 0);

        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(curAttackTime > 0)
                curAttackTime -= Time.deltaTime;
            else
            {
                other.gameObject.GetComponent<Player>().Damage(damage);
                curAttackTime = attackTime;
            }
        }
    }
}

