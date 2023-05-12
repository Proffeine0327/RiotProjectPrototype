using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager manager { get; private set; }

    [SerializeField] private InteractUI interactUI;
    [SerializeField] private JoystickUI joystickUI;
    [SerializeField] private DragUI dragUI;

    public InteractUI InteractUI => interactUI;
    public JoystickUI JoystickUI => joystickUI;
    public DragUI DragUI => dragUI;

    private void Awake() 
    {
        manager = this;    
    }
}
