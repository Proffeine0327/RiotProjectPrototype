using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path path { get; private set; }

    [SerializeField] private List<Transform> points = new List<Transform>();

    private void Awake() 
    {
        path = this;    
    }
}
