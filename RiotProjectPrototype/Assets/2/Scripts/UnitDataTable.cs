using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "unit/table", fileName = "UnitTable")]
public class UnitDataTable : ScriptableObject
{
    [SerializeField] private List<UnitData> units = new List<UnitData>();

    public List<UnitData> Units => units;
}

[System.Serializable]
public struct UnitData
{
    [SerializeField] private string name;
    [SerializeField] private GameObject prefeb;
    [SerializeField] private int cost;

    public string Name => name;
    public GameObject Prefeb => prefeb;
    public int Cost => cost;
}