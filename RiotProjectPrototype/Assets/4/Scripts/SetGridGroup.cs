using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGridGroup : MonoBehaviour
{
    public static SetGridGroup group { get; private set; }

    [SerializeField] private Vector2Int xySize;
    [SerializeField] private List<RowList> cols = new List<RowList>();

    public void ActiveGrid(bool active)
    {
        foreach(var col in cols) foreach(var obj in col.Rows) obj.SetActive(active);
    }

    public Vector2Int XYSize => xySize;
    public List<RowList> Cols => cols;

    private void Awake() 
    {
        group = this;
    }
}

[System.Serializable]
public class RowList
{
    [SerializeField] private List<GameObject> rows = new List<GameObject>();
    public List<GameObject> Rows => rows;
}
