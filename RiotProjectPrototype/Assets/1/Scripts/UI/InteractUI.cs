using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace first
{
    public class InteractUI : MonoBehaviour
    {
        [SerializeField] private Button btn;

        private float time;

        public void DisplayUI(System.Action ac, float t)
        {
            time = t;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => ac?.Invoke());
        }

        private void Update()
        {
            if (GameManager.manager.IsStartGame)
            {
                btn.gameObject.SetActive(false);
                return;
            }

            if (time >= 0)
            {
                time -= Time.deltaTime;
                btn.gameObject.SetActive(true);
            }
            else btn.gameObject.SetActive(false);
        }
    }
}