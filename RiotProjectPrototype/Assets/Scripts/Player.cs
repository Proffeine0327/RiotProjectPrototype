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
    [SerializeField] private float groundCastRadius;
    [SerializeField] private Vector3 groundCastOffset;
    [SerializeField] private LayerMask groundCastLayer;
    [Header("Camera")]
    [SerializeField] private Transform camPivot;
    [SerializeField] private Vector2 camSensivity;

    private Rigidbody rb;
    private float camRotationX;

    public void Damage(float amount, Vector3 targetpos)
    {
        if(!isInvincible)
        {
            var pos = transform.position - targetpos;
            pos.y = 0;
            pos = pos.normalized;
            pos.y = 1.5f;

            rb.AddForce(pos * 200);
            hp -= amount;
        }
    }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        player = this;

        hp = maxHp;

        rb = GetComponent<Rigidbody>();
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
        var isGround = Physics.CheckSphere(transform.position + groundCastOffset, groundCastRadius, groundCastLayer);

        var dir = transform.TransformDirection(new Vector3(h, 0, v).normalized * moveSpeed);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            dir *= 1.5f;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 70, Time.deltaTime * 10);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime * 10);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector3.up * jumpScale, ForceMode.Impulse);
            transform.Translate(0, jumpScale * Time.deltaTime, 0);
        }
        
        transform.Translate(dir * Time.deltaTime, Space.World);
    }

    private void CameraMove()
    {
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        transform.Rotate(0, x * camSensivity.x * Time.deltaTime, 0);
        
        camRotationX -= y * Time.deltaTime * camSensivity.y;
        camRotationX = Mathf.Clamp(camRotationX, -90, 90);
        camPivot.localRotation = Quaternion.Euler(camRotationX, 0, 0);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + groundCastOffset, groundCastRadius);    
    }
}
