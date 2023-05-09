using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;

public class ButtonUIAddEditorWindow : EditorWindow
{
    public static void Open(List<string> tableKeys, Action<string> action)
    {
        var window = EditorWindow.CreateInstance<ButtonUIAddEditorWindow>();
        window.buttonkeys = tableKeys;
        window.action = action;

        var mousePos = EditorGUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        window.ShowAsDropDown(new Rect(mousePos, Vector2.zero), new Vector2(250,20));
    }

    Action<string> action;
    string key;
    List<string> buttonkeys;

    private void OnGUI() 
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("key : ", GUILayout.Width(30));
        key = EditorGUILayout.TextField(key);

        EditorGUI.BeginDisabledGroup(buttonkeys.Contains(key));
        if((GUILayout.Button("Add", GUILayout.MaxWidth(40)) || Event.current.keyCode == KeyCode.Return) && !buttonkeys.Contains(key))
        {
            action.Invoke(key);
            this.Close();
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
    }
}
#endif