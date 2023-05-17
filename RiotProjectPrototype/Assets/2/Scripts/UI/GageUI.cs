using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageUI : MonoBehaviour
{
    [SerializeField] private Text txt;

    private Image img;

    private void Awake() 
    {
        img = GetComponent<Image>();
    }

    private void Update() 
    {
        var gage = InGameCardManager.manager.Gage;
        var intGage = (int)gage;
        img.fillAmount = Mathf.Lerp(intGage, intGage + 1, Ease((gage - intGage) / 1)) / 10f;

        txt.text = intGage.ToString();
    }

    private float Ease(float x) => 1 - Mathf.Pow(1 - x, 4);
}
