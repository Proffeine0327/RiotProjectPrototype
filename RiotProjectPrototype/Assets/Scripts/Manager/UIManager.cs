using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager manager { get; private set; }

    [SerializeField] private JoystickUI joystickUI;
    [SerializeField] private ButtonUI buttonUI;
    
    public JoystickUI JoystickUI { get { return joystickUI; } }
    public ButtonUI ButtonUI { get { return buttonUI; } }

    private void Awake() 
    {
        manager = this;
    }
}
