using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridState
{
    ground,
    road,
    blocked
}

public class GridPlane : MonoBehaviour
{
    [SerializeField] private GridState state;
    [SerializeField] private Vector2Int yxindex;

    public GridState State => state;
    public Vector2Int YXIndex => yxindex;
    
    public void Init(GridState state, Vector2Int yxindex)
    {
        this.state = state;
        this.yxindex = yxindex;
    }
}
