using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace first
{
    public class RemainUI : MonoBehaviour
    {
        [SerializeField] private Text txt;

        private void Update()
        {
            txt.text = $"Remain : {25 - Player.player.KillAmount}";

            if (!GameManager.manager.IsStartGame) txt.text = "";
        }
    }
}
