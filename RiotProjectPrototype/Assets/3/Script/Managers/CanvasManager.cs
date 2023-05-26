using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager manager { get; private set; }

    [SerializeField] private HpBarUIManager hpBarUIManager;
    [SerializeField] private NextStageUI nextStageUI;

    public HpBarUIManager HpBarUIManager => hpBarUIManager;
    public NextStageUI NextStageUI => nextStageUI;

    private void Awake() 
    {
        manager = this;
    }
}
