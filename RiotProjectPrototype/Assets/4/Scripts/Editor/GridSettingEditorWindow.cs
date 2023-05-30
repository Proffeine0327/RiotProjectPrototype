using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class GridSettingEditorWindow : EditorWindow
{
    public static void ShowWindow(Vector2Int size, Action<Material[], string[], int[,]> onSubmit)
    {
        var window = GetWindow<GridSettingEditorWindow>();
        window.titleContent = new GUIContent("GridSettingWindow");
        window.size = new Vector2Int(size.x, size.y);
        window.table = new int[size.y, size.x];
        window.isAlreadyAdded = new bool[size.y, size.x];
        window.onSubmit = onSubmit;
        window.Show();
    }

    private Material[] materials = new Material[3];
    private string[] tags = new string[3] { "Untagged", "Untagged", "Untagged" };

    private Vector2Int size;
    private Action<Material[], string[], int[,]> onSubmit;
    private int[,] table;
    private bool[,] isAlreadyAdded;

    private void OnGUI()
    {
        if (GUILayout.Button("Submit", GUILayout.Width(100))) Submit();
        EditorGUILayout.Space(10);

        var tagbtnstyle = new GUIStyle(EditorStyles.popup);
        tagbtnstyle.alignment = TextAnchor.MiddleLeft;
        tagbtnstyle.fixedWidth = 200;

        EditorGUILayout.BeginHorizontal();
        materials[0] = (Material)EditorGUILayout.ObjectField("Ground Material", materials[0], typeof(Material), false, GUILayout.Width(400));
        EditorGUILayout.LabelField("Tag", GUILayout.Width(30));

        if (GUILayout.Button(tags[0], tagbtnstyle)) ShowTagMenu(0);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        materials[1] = (Material)EditorGUILayout.ObjectField("Road Material", materials[1], typeof(Material), false, GUILayout.Width(400));
        EditorGUILayout.LabelField("Tag", GUILayout.Width(30));
        if (GUILayout.Button(tags[1], tagbtnstyle)) ShowTagMenu(1);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        materials[2] = (Material)EditorGUILayout.ObjectField("Blocked Material", materials[2], typeof(Material), false, GUILayout.Width(400));
        EditorGUILayout.LabelField("Tag", GUILayout.Width(30));
        if (GUILayout.Button(tags[2], tagbtnstyle)) ShowTagMenu(2);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Field", new GUIStyle(EditorStyles.boldLabel) { fontSize = 14 });

        // <----------------------- box control ----------------------->
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                if (table[i, j] == 0) GUI.backgroundColor = Color.green;
                if (table[i, j] == 1) GUI.backgroundColor = Color.yellow;
                if (table[i, j] == 2) GUI.backgroundColor = Color.red;

                var curRect = new Rect(25 * j, 130 + 25 * i, 24, 24);
                if (curRect.Contains(Event.current.mousePosition))
                {
                    if (!isAlreadyAdded[i, j] && (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown))
                    {
                        table[i, j]++;
                        isAlreadyAdded[i, j] = true;
                    }
                    if (table[i, j] > 2) table[i, j] = 0;
                }

                GUI.Box(curRect, "");
            }
        }

        GUI.backgroundColor = Color.white;
        if (GUI.Button(new Rect(0, 130 + 25 * size.y, 100, 20), "Reset")) for (int i = 0; i < size.y; i++) for (int j = 0; j < size.x; j++) table[i, j] = 0;
        if (GUI.Button(new Rect(120, 130 + 25 * size.y, 100, 20), "Save"))
        {
            var saveobj = new GridSettingDatas();
            saveobj.materials = materials;
            saveobj.tags = tags;

            for(int i = 0; i < table.GetLength(0); i++)
            {
                saveobj.table.Add(new GridSettingDataTableRow());
                for(int j = 0; j < table.GetLength(1); j++) saveobj.table[i].row.Add(table[i, j]);
            }
            var content = JsonUtility.ToJson(saveobj);
            string directory = EditorUtility.OpenFolderPanel("Select Directory", Application.dataPath, "");

            var time = DateTime.Now.ToString("[yyyy-MM-dd HH-mm-ss]");
            File.WriteAllText($"{directory}/GridSetting{time}.json", content);
            AssetDatabase.Refresh();
        }
        if (GUI.Button(new Rect(220, 130 + 25 * size.y, 100, 20), "Load"))
        {
            string directory = EditorUtility.OpenFilePanel("Select Directory", Application.dataPath, "json");
            string content = File.ReadAllText(directory);
            var loadobj = JsonUtility.FromJson<GridSettingDatas>(content);

            materials = loadobj.materials;
            tags = loadobj.tags;

            for(int i = 0; i < Mathf.Min(size.y, loadobj.table.Count); i++) 
                for(int j = 0; j < Mathf.Min(size.x, loadobj.table[0].row.Count); j++) 
                    table[i, j] = loadobj.table[i].row[j];
        }

        if (Event.current.type == EventType.MouseUp)
            for (int i = 0; i < size.y; i++) for (int j = 0; j < size.x; j++) isAlreadyAdded[i, j] = false;

        Repaint();
    }

    private void Submit()
    {
        foreach (var material in materials) if (material == null) return;
        onSubmit?.Invoke(materials, tags, table);
        this.Close();
    }

    private void ShowTagMenu(int targetid)
    {
        var menu = new GenericMenu();
        foreach (var tag in InternalEditorUtility.tags) menu.AddItem(new GUIContent(tag), false, () => tags[targetid] = tag);
        menu.ShowAsContext();
    }
}

[Serializable]
public class GridSettingDatas
{
    public Material[] materials;
    public string[] tags;
    public List<GridSettingDataTableRow> table = new List<GridSettingDataTableRow>();
}

[Serializable]
public class GridSettingDataTableRow
{
    public List<int> row = new List<int>();
}
#endif