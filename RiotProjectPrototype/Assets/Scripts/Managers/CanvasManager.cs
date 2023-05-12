using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager manager { get; private set; }

    [SerializeField] private InteractUI interactUI;

    public InteractUI InteractUI => interactUI;

    private void Awake() 
    {
        manager = this;    
    }
}
