using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    private bool isOnAttackField;

    public bool IsOnAttackField => isOnAttackField;

    public void Dragging()
    {
        
    }

    private void Update() 
    {
        if(Physics.Raycast(transform.position, Vector3.down, out var hit , 2, LayerMask.GetMask("Ground")))    
        {
            if(hit.collider.CompareTag("AttackField"))
            {
                isOnAttackField = true;
            }
            else
            {
                isOnAttackField = false;
            }
        }
    }
}
