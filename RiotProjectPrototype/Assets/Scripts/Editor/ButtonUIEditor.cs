using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ButtonUI))]
public class ButtonUIEditor : Editor
{
    ReorderableList buttonList;
    int selectedIndex;

    private void OnEnable()
    {
        buttonList = new ReorderableList(serializedObject, serializedObject.FindProperty("keys"), true, true, true, false);
        buttonList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Buttons");
        buttonList.drawElementCallback = (rect, index, active, focus) =>
        {
            var i = index;

            var keyelement = buttonList.serializedProperty.GetArrayElementAtIndex(i);
            var valueelement = serializedObject.FindProperty("values").GetArrayElementAtIndex(i);
            rect.y += 2;

            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width * 0.15f, EditorGUIUtility.singleLineHeight), $"Element {i}: ");

            EditorGUI.BeginChangeCheck();
            var str = EditorGUI.DelayedTextField
            (
                new Rect(rect.x + rect.width * 0.15f, rect.y, rect.width * 0.25f, EditorGUIUtility.singleLineHeight),
                keyelement.stringValue
            );
            if(EditorGUI.EndChangeCheck())
            {
                if(!(target as ButtonUI).buttons.ContainsKey(str))
                    keyelement.stringValue = str;
            }

            valueelement.objectReferenceValue = EditorGUI.ObjectField
            (
                new Rect(rect.x + rect.width * 0.4f + 5f, rect.y, rect.width * 0.6f - 25f, EditorGUIUtility.singleLineHeight),
                valueelement.objectReferenceValue,
                typeof(RectTransform),
                true
            );

            if (GUI.Button(new Rect(rect.x + rect.width - 20f, rect.y, 20f, EditorGUIUtility.singleLineHeight), "-"))
            {
                serializedObject.FindProperty("keys").DeleteArrayElementAtIndex(i);
                serializedObject.FindProperty("values").DeleteArrayElementAtIndex(i);
            }
        };

        buttonList.onAddCallback = (list) =>
        {
            ButtonUIAddEditorWindow.Open((target as ButtonUI).buttons.Keys.ToList(), (key) =>
            {
                serializedObject.FindProperty("keys").arraySize++;
                serializedObject.FindProperty("keys").GetArrayElementAtIndex(serializedObject.FindProperty("keys").arraySize - 1).stringValue = key;
                serializedObject.FindProperty("values").arraySize++;
                serializedObject.FindProperty("values").GetArrayElementAtIndex( serializedObject.FindProperty("values").arraySize - 1).objectReferenceValue = null;

                serializedObject.ApplyModifiedProperties();
            });
        };

        buttonList.onSelectCallback = list => {
            selectedIndex = list.index;
        };

        buttonList.onReorderCallback = list => {
            var i = list.index;

            serializedObject.FindProperty("values").MoveArrayElement(selectedIndex, i);
            serializedObject.ApplyModifiedProperties();
        };
    }

    public override void OnInspectorGUI()
    {
        buttonList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif