using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeSlotUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Player.PlayerUpgrade upgrade;
    [SerializeField] private bool isSelected;

    public Player.PlayerUpgrade Upgrade { get { return upgrade; } }
    public bool IsSelected { get { return isSelected; } }

    public void Init(Player.PlayerUpgrade upgrade)
    {
        this.upgrade = upgrade;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
    }
}
