using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarUIManager : MonoBehaviour
{
    [SerializeField] private GameObject hpBarUIPrefeb;

    public void MakeHpBar(Unit target)
    {
        var ui = Instantiate(hpBarUIPrefeb, transform);
        ui.GetComponent<HpBarUI>().SetUnit(target);
    }
}
