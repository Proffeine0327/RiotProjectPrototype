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
        Attack();
    }

    private void Move()
    {
        var vel = new Vector3(UIManager.manager.JoystickUI.Value.x, 0, UIManager.manager.JoystickUI.Value.y).normalized;
        vel = transform.TransformDirection(vel) * moveSpeed;
        vel.y = rb.velocity.y;

        if(UIManager.manager.ButtonUI.GetButtonDown("JumpButton"))
            vel.y = jumpScale;
        
        rb.velocity = vel;
    }

    private void Attack()
    {
        if(UIManager.manager.ButtonUI.GetButtonDown("SkillButton"))
        {
            Debug.Log("Attack!");
        }
    }
}
