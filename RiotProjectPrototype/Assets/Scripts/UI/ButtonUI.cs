using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private List<string> keys = new List<string>();
    [SerializeField] private List<RectTransform> values = new List<RectTransform>();
    public Dictionary<string, RectTransform> buttons = new Dictionary<string, RectTransform>();
    public Dictionary<string, bool> buttonPressTable = new Dictionary<string, bool>();

    public bool GetButtonDown(string key) => buttonPressTable[key];

    private void Start()
    {
        buttonPressTable = new Dictionary<string, bool>();
        foreach (var key in keys)
            buttonPressTable.Add(key, false);
    }

    private void Update()
    {
        foreach (var key in keys)
            if (buttonPressTable[key]) buttonPressTable[key] = false;

        var screenRatio = new Vector2(1920f / Screen.width, 1080f / Screen.height);

        // foreach(var key in keys) for(int i = 0; i < Input.touchCount; i++)
        // {
        //     if(buttons[key].rect.Contains(Input.GetTouch(i).position * screenRatio))
        //         buttonPressTable[key] = true;
        // }

        foreach (var key in keys)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var buttonrect = new Rect((Vector2)buttons[key].localPosition + new Vector2(960, 540) - buttons[key].sizeDelta / 2, buttons[key].sizeDelta);
                if (buttonrect.Contains(Input.mousePosition * screenRatio))
                    buttonPressTable[key] = true;
            }
        }
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var kvp in buttons)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        buttons = new Dictionary<string, RectTransform>();

        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
            buttons.Add(keys[i], values[i]);
    }
}
