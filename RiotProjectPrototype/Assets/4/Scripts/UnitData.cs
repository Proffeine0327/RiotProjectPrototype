using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "RiotProjectPrototype/UnitData", order = 0)]
public class UnitData : ScriptableObject 
{
    [SerializeField] private string unitname;
    [SerializeField] private float maxHp;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject prefeb;
    [SerializeField] private UnitType type;
    [SerializeField] private GridState setableGrid;
    [Header("ui")]
    [SerializeField] private Sprite cardsprite;

    public string Unitname => unitname;
    public float MaxHp => maxHp;
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    public GameObject Prefeb => prefeb;
    public UnitType Type => type;
    public GridState SetableGrid => setableGrid;
    public Sprite Cardsprite => cardsprite;
}

public enum UnitType
{
    normal,
    trap
}
