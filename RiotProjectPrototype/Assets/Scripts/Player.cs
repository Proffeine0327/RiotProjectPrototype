using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player { get; private set; }

    [Header("Hp")]
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    [SerializeField] private bool isInvincible;
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float jumpScale;
    [Header("Camera")]
    [SerializeField] private Transform camPivot;
    [SerializeField] private Vector2 camSensivity;

    private CharacterController cc;
    private float Yvelocity;
    private float camRotationX;

    public void Damage(float amount)
    {
        if(!isInvincible)
            hp -= amount;
    }

    private void Awake()
    {
        player = this;

        hp = maxHp;

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

        var velocity = transform.TransformDirection(new Vector3(h, 0, v).normalized * moveSpeed);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            velocity *= 1.5f;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 70, Time.deltaTime * 10);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime * 10);
        }
        
        if(!cc.isGrounded) Yvelocity -= 9.8f * gravityMultiplier * Time.deltaTime;
        else Yvelocity = 0;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Yvelocity = jumpScale;
            transform.Translate(0, jumpScale * Time.deltaTime, 0);
        }
        
        velocity.y = Yvelocity;
        
        cc.Move(velocity * Time.deltaTime);
    }

    private void CameraMove()
    {
        var x = Input.GetAxisRaw("Mouse X");
        var y = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(0, x * camSensivity.x * Time.deltaTime, 0);
        
        camRotationX -= y * Time.deltaTime * camSensivity.y;
        camRotationX = Mathf.Clamp(camRotationX, -90, 90);
        camPivot.localRotation = Quaternion.Euler(camRotationX, 0, 0);
    }
}
