using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private Text txt;

    private float time;

    public void DisplayUI(string text, float t)
    {
        time = t;
        txt.text = $"[E] : {text}";
    }

    private void Update() 
    {
        if(time >= 0)
        {
            time -= Time.deltaTime;
            txt.enabled = true;
        }
        else txt.enabled = false;    
    }
}
