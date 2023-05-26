using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Image img;
    private Unit targetUnit;

    public void SetUnit(Unit target)
    {
        targetUnit = target;
    }

    void Update()
    {
        if(targetUnit == null) 
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Camera.main.WorldToScreenPoint(targetUnit.transform.position + new Vector3(0, 0, -1));
        img.fillAmount = targetUnit.CurHp / targetUnit.MaxHp;
    }
}
