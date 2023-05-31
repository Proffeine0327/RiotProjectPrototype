using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGroup : MonoBehaviour
{
    public static GridGroup group { get; private set; }

    [SerializeField] private Vector2Int xySize;
    [SerializeField] private List<RowList> cols = new List<RowList>();

    public void ActiveGrid(GridState state)
    {
        foreach(var col in cols) foreach(var obj in col.Rows)
            if(obj.State == state) obj.gameObject.SetActive(true);
            else obj.gameObject.SetActive(false);
    }

    public void DisableAllGrid()
    {
        foreach(var col in cols) foreach(var obj in col.Rows) obj.gameObject.SetActive(false);
    }

    public Vector2Int XYSize => xySize;
    public List<RowList> Cols => cols;

    private void Awake() 
    {
        group = this;

        DisableAllGrid();
    }
}

[System.Serializable]
public class RowList
{
    [SerializeField] private List<GridPlane> rows = new List<GridPlane>();
    public List<GridPlane> Rows => rows;
}
