using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    
    private void Start() 
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            var randompos = Random.insideUnitSphere * 25;
            randompos.y = 2;
            
            Instantiate(enemy, randompos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.5f, 4));
        }
    }
}
