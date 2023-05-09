using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpScale;

    private Rigidbody rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Jump();
        Attack();
    }

    private void Move()
    {
        var dir = new Vector3(UIManager.manager.JoystickUI.Value.x, 0, UIManager.manager.JoystickUI.Value.y).normalized;
        dir = transform.TransformDirection(dir);

        dir.y = rb.velocity.y;

        rb.velocity = dir * moveSpeed;
    }

    private void Jump()
    {
        if(UIManager.manager.ButtonUI.GetButtonDown("JumpButton"))
        {
            Debug.Log("Jump!");
        }
    }

    private void Attack()
    {
        if(UIManager.manager.ButtonUI.GetButtonDown("AttackButton"))
        {
            Debug.Log("Attack!");
        }
    }
}
