using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace first
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager manager { get; private set; }

        [SerializeField] private bool isStartGame;

        public bool IsStartGame => isStartGame;

        private void Awake()
        {
            manager = this;
        }

        private void Start()
        {
            StartCoroutine(GameRoutine());
        }

        private IEnumerator GameRoutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => CanvasManager.manager.DecisionUI.Check());
                CanvasManager.manager.DecisionUI.Display(false);
                isStartGame = true;

                EnemySpawnManager.manager.StartSpawning(25);

                yield return new WaitUntil(() => Player.player.KillAmount >= 25);
                CanvasManager.manager.DecisionUI.Display(true);
                isStartGame = false;
            }
        }
    }
}
