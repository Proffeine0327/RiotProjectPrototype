using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextStageUI : MonoBehaviour
{
    [SerializeField] private GameObject bg;
    [SerializeField] private Button btn;

    private bool isShowingUI;

    public bool IsShowingUI => isShowingUI;

    public void DisplayUI()
    {
        isShowingUI = true;
        bg.SetActive(true);
        btn.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("3/Scene/SampleScene");
        });
    }
}
