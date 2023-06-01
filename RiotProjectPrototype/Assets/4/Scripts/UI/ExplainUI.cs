using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplainUI : MonoBehaviour
{
    public static ExplainUI ui { get; private set; }

    private Text txt;
    private float time;

    public void DisplayUI(string str, float t)
    {
        txt.text = str;
        time = t;
        txt.color = Color.black;
    }

    public void DisplayUI(string str, float t, Color color)
    {
        txt.text = str;
        time = t;
        txt.color = color;
    }

    private void Awake() 
    {
        ui = this;
        txt = GetComponent<Text>();
    }

    private void Update() 
    {
        if(time > 0)
        {
            txt.enabled = true;
            time -= Time.deltaTime;
        }
        else txt.enabled = false;
    }
}
