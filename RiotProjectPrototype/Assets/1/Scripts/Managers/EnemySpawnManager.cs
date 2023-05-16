using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace first
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public static EnemySpawnManager manager { get; private set; }

        [SerializeField] private List<SpawnInfo> spawnInfos = new List<SpawnInfo>();
        [SerializeField] private GameObject enemyPrefeb;

        private void Awake()
        {
            manager = this;
        }

        public void StartSpawning(int amount) => StartCoroutine(SpawnRoutine(amount));

        private IEnumerator SpawnRoutine(int amount)
        {
            var enemys = new List<GameObject>();

            for (int i = 0; i < amount; i++)
            {
                yield return new WaitForSeconds(Random.Range(0.4f, 2f));
                var random = Random.Range(0, spawnInfos.Count);

                var enemy = Instantiate(enemyPrefeb, spawnInfos[random].SpawnPos, Quaternion.identity);
                enemy.GetComponent<Enemy>().Init(spawnInfos[random].Shutter);
                enemys.Add(enemy);
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
}
