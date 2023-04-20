using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager manager { get; private set; }
    
    [SerializeField] private UpgradeUI upgradeUI;

    public UpgradeUI UpgradeUI { get { return upgradeUI; } }

    private void Awake() 
    {
        manager = this;
    }
}
