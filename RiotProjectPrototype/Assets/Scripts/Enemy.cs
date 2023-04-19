using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackTime;

    private void Update()
    {
        var diff = Player.player.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg, 0);

        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }
}

