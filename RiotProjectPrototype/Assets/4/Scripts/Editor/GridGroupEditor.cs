using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(GridGroup))]
public class GridGroupEditor : Editor
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
                var comp = (target as GridGroup);
                var cols = comp.Cols;

                for (int i = cols.Count - 1; i >= 0; i--)
                {
                    var rows = cols[i].Rows;
                    for (int j = rows.Count - 1; j >= 0; j--) if (rows[j] != null) DestroyImmediate(rows[j].gameObject);
                }
                cols.Clear();

                for (int y = 0; y < xysizevector.y; y++)
                {
                    cols.Add(new RowList());
                    for (int x = 0; x < xysizevector.x; x++)
                    {
                        var obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
                        obj.transform.localScale = Vector3.one * 0.098f;
                        obj.transform.SetParent((target as GridGroup).transform);
                        obj.transform.localPosition = new Vector3((x - (xysizevector.x / 2f) + 0.5f) * -1, 0, y - (xysizevector.y / 2f) + 0.5f);
                        obj.name = $"Grid ({y},{x})";

                        obj.tag = tags[states[y, x]];
                        obj.GetComponent<MeshRenderer>().sharedMaterial = materials[states[y, x]];
                        var gridplane = obj.AddComponent<GridPlane>();
                        gridplane.Init((GridState)states[y, x], new Vector2Int(y, x));

                        cols[y].Rows.Add(gridplane);
                    }
                }
                EditorUtility.SetDirty(target);
            });
        }
        if (GUILayout.Button("Clear"))
        {
            var comp = (target as GridGroup);
            var cols = comp.Cols;
            for (int i = cols.Count - 1; i >= 0; i--)
            {
                var rows = cols[i].Rows;
                for (int j = rows.Count - 1; j >= 0; j--) if (rows[j] != null) DestroyImmediate(rows[j].gameObject);
            }
            cols.Clear();
            EditorUtility.SetDirty(target);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif