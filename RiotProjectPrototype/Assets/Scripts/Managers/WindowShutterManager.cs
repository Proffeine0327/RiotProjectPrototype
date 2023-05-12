using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowShutterManager : MonoBehaviour
{
    public static WindowShutterManager manager { get; private set; }

    

    private void Awake() 
    {
        manager = this;
    }

    private void Update() 
    {
        
    }
}
