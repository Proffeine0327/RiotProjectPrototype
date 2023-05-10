using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player { get; private set; }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravityScale;
    [SerializeField] private float jumpScale;

    private CharacterController cc;
    private float yVelocity;

    private void Awake() 
    {
        player = this;

        cc.GetComponent<CharacterController>();    
    }

    private void Update() 
    {
        Move();
        CameraMove();
    }

    private void Move()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var velocity = new Vector3(h, 0, v);
        velocity = transform.TransformDirection(velocity).normalized * moveSpeed;

        if(!cc.isGrounded)
        {
            yVelocity -= 9.8f * gravityScale * Time.deltaTime;
            velocity.y = yVelocity;
        }

        cc.Move(velocity * Time.deltaTime);
    }

    private void CameraMove()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
    }
}
