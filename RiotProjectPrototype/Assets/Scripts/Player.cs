using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player { get; private set; }

    [Header("move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravityScale;
    [Header("aim")]
    [SerializeField] private Vector2 aimSensivity;
    [SerializeField] private Camera mainCam;
    [Header("interact")]
    [SerializeField] private float interactDist;

    private CharacterController cc;

    private Vector2 aimVec;
    private float yVelocity;

    public float InteractDist => interactDist;

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
        Interact();
    }

    private void Move()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var velocity = new Vector3(h, 0, v);
        velocity = transform.TransformDirection(velocity).normalized * moveSpeed;

        if (!cc.isGrounded) yVelocity -= 9.8f * gravityScale * Time.deltaTime;
        else yVelocity = 0;

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

        transform.localRotation = Quaternion.Euler(0, aimVec.x, 0);
        mainCam.transform.localRotation = Quaternion.Euler(aimVec.y, 0, 0);
    }

    private void Interact()
    {
        RaycastHit hit;
        Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, interactDist, ~LayerMask.GetMask("Player"));

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Interactable"))
        {
            hit.collider.GetComponent<IInteractable>().DisplayUI();
            if (Input.GetKeyDown(KeyCode.E))
                hit.collider.GetComponent<IInteractable>().Interact();
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(mainCam.transform.position, mainCam.transform.position +  mainCam.transform.forward * interactDist);    
    }
}
