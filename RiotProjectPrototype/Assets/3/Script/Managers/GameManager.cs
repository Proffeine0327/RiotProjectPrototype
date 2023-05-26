using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    void Update()
    {
        if(enemies.Count == 0) return;
        if(CanvasManager.manager.NextStageUI.IsShowingUI) return;

        bool isEnemyAllDied = true;
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] == null) continue;
            else isEnemyAllDied = false;
        }
        if(isEnemyAllDied) CanvasManager.manager.NextStageUI.DisplayUI();
    }
}
