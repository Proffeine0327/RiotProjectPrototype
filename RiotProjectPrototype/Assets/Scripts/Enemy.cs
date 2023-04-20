using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackTime;
    [SerializeField] private float damage;

    private Rigidbody rb;

    private float curAttackTime;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var diff = Player.player.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg, 0);

        var velocity = (transform.forward * moveSpeed);
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;

        if(curAttackTime > 0)
            curAttackTime -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(curAttackTime <= 0)
            {
                other.gameObject.GetComponent<Player>().Damage(damage, transform.position);
                curAttackTime = attackTime;
            }
        }
    }
}

