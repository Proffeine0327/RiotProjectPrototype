using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGridGroup : MonoBehaviour
{
    [SerializeField] private List<RowList> cols = new List<RowList>();
}

[System.Serializable]
public class RowList
{
    [SerializeField] private List<SetGrid> rows = new List<SetGrid>();
    public List<SetGrid> Rows => rows;
}
