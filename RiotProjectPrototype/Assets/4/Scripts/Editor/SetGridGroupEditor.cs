using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(SetGridGroup))]
public class SetGridGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var xysize = serializedObject.FindProperty("xySize");
        EditorGUILayout.PropertyField(xysize);
        EditorGUILayout.Space(10);

        if (GUILayout.Button("Spawn Grid"))
        {
            var xysizevector = xysize.vector2IntValue;
            GridSettingEditorWindow.ShowWindow(xysizevector, (materials, tags, states) =>
            {
                var comp = (target as SetGridGroup);
                var cols = comp.Cols;

                for (int i = cols.Count - 1; i >= 0; i--)
                {
                    var rows = cols[i].Rows;
                    for (int j = rows.Count - 1; j >= 0; j--) if (rows[j] != null) DestroyImmediate(rows[j]);
                }
                cols.Clear();

                for (int y = 0; y < xysizevector.y; y++)
                {
                    cols.Add(new RowList());
                    for (int x = 0; x < xysizevector.x; x++)
                    {
                        var obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
                        obj.transform.localScale = Vector3.one * 0.09f;
                        obj.transform.SetParent((target as SetGridGroup).transform);
                        obj.transform.localPosition = new Vector3((x - (xysizevector.x / 2f) + 0.5f) * -1, 0, y - (xysizevector.y / 2f) + 0.5f);
                        obj.name = $"Grid ({(x - (xysizevector.x / 2f) + 0.5f) * -1}, {y - (xysizevector.y / 2f) + 0.5f})";

                        obj.tag = tags[states[y, x]];
                        obj.GetComponent<MeshRenderer>().sharedMaterial = materials[states[y, x]];

                        cols[y].Rows.Add(obj);
                    }
                }
                EditorUtility.SetDirty(target);
            });
        }
        if (GUILayout.Button("Clear"))
        {
            var comp = (target as SetGridGroup);
            var cols = comp.Cols;
            for (int i = cols.Count - 1; i >= 0; i--)
            {
                var rows = cols[i].Rows;
                for (int j = rows.Count - 1; j >= 0; j--) if (rows[j] != null) DestroyImmediate(rows[j]);
            }
            cols.Clear();
            EditorUtility.SetDirty(target);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif