using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "RiotProjectPrototype/UnitData", order = 0)]
public class UnitData : ScriptableObject 
{
    [SerializeField] private float maxHp;
    [SerializeField] private float attackSpeed;
    [SerializeField] private GameObject prefeb;
    [SerializeField] private UnitType type;

    public float MaxHp => maxHp;
    public float AttackSpeed => attackSpeed;
    public GameObject Prefeb => prefeb;
    public UnitType Type => type;
}

public enum UnitType
{
    normal,
    trap
}
