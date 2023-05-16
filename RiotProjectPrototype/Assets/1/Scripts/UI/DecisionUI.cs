using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace first
{
    public class DecisionUI : MonoBehaviour
    {
        [SerializeField] private Button btn;
        private bool isClicked;

        private void Awake()
        {
            btn.onClick.AddListener(() => isClicked = true);
        }

        public void Display(bool active) => btn.gameObject.SetActive(active);

        public bool Check()
        {
            var temp = isClicked;
            if (isClicked) isClicked = false;
            return temp;
        }
    }
}
