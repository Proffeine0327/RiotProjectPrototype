using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStarUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> starSprites = new List<Sprite>();

    private Image img;
    private PlayerUnit targetUnit;

    private void Awake() 
    {
        img = GetComponent<Image>();    
    }

    public void Init(PlayerUnit target)
    {
        this.targetUnit = target;
    }

    private void Update() 
    {
        if(targetUnit == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Camera.main.WorldToScreenPoint(targetUnit.transform.position) + Vector3.up * 200;
        img.sprite = starSprites[targetUnit.Lvl - 1];
    }
}
