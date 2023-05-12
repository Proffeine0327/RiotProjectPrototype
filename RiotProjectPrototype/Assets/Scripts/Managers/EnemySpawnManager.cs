using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager manager { get; private set; }

    [SerializeField] private List<SpawnInfo> spawnInfos = new List<SpawnInfo>();
    [SerializeField] private GameObject enemyPrefeb;

    private void Awake()
    {
        manager = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            var random = Random.Range(0, spawnInfos.Count);

            var enemy = Instantiate(enemyPrefeb, spawnInfos[random].SpawnPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(spawnInfos[random].Shutter);
        }
    }
}

[System.Serializable]
public class SpawnInfo
{
    [SerializeField] private Shutter shutter;
    [SerializeField] private Vector3 spawnPos;

    public Shutter Shutter => shutter;
    public Vector3 SpawnPos => spawnPos;
}
