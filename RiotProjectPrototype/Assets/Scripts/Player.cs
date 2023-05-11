using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player { get; private set; }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravityScale;
    [SerializeField] private float jumpScale;
    [Header("aim")]
    [SerializeField] private Vector2 aimSensivity;
    [SerializeField] private Camera mainCam;

    private CharacterController cc;
    private float yVelocity;
    private Vector2 aimVec;

    private void Awake() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = this;

        cc = GetComponent<CharacterController>();    
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

        if(!cc.isGrounded) yVelocity -= 9.8f * gravityScale * Time.deltaTime;
        else yVelocity = 0;

        if(Input.GetKeyDown(KeyCode.Space))
            yVelocity = jumpScale;

        velocity.y = yVelocity;

        cc.Move(velocity * Time.deltaTime);
    }

    private void CameraMove()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        aimVec.x += mouseX * aimSensivity.x * Time.deltaTime;
        aimVec.y -= mouseY * aimSensivity.y * Time.deltaTime;
        aimVec.y = Mathf.Clamp(aimVec.y, -85, 85);

        transform.rotation = Quaternion.Euler(0, aimVec.x, 0);
        mainCam.transform.localRotation = Quaternion.Euler(aimVec.y, 0, 0);
    }
}
