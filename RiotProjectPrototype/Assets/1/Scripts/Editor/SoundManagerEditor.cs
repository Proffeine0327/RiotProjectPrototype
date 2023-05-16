#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace first
{
    [CustomEditor(typeof(SoundManager))]
    public class SoundManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Add"))
            {
                var keys = serializedObject.FindProperty("keys");
                var values = serializedObject.FindProperty("values");

                keys.arraySize++;
                keys.GetArrayElementAtIndex(keys.arraySize - 1).stringValue = "";
                values.arraySize++;
                values.GetArrayElementAtIndex(values.arraySize - 1).objectReferenceValue = null;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif