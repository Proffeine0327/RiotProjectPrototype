using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGridGroup : MonoBehaviour
{
    [SerializeField] private Vector2Int xySize;
    [SerializeField] private List<RowList> cols = new List<RowList>();

    public Vector2Int XYSize => xySize;
    public List<RowList> Cols => cols;
}

[System.Serializable]
public class RowList
{
    [SerializeField] private List<GameObject> rows = new List<GameObject>();
    public List<GameObject> Rows => rows;
}
