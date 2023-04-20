using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float speed;
    private float damage;
    private Vector3 dir;

    public void Init(float damage, Vector3 dir)
    {
        this.damage = damage;
        this.dir = dir;
    }

    private void Update() 
    {
        var hits = Physics.OverlapSphere(transform.position, range * transform.lossyScale.x);
        foreach (var hit in hits)
        {
            if(hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>().Damage(damage);
                Destroy(gameObject);
            }
        }

        transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range * transform.lossyScale.x);
    }
}
