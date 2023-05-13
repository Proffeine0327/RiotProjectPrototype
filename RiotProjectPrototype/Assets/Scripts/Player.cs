using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    [Header("Kill")]
    [SerializeField] private int killAmount;
    [SerializeField] private Animator gunAnim;
    [SerializeField] private float shootTime;

    private CharacterController cc;

    private Vector2 aimVec;
    private float yVelocity;
    private float curShootTime;

    public float InteractDist => interactDist;
    public int KillAmount => killAmount;

    public void AddKillAmount() => killAmount++;

    private void Awake()
    {
        player = this;

        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        CameraMove();
        Interact();
        KillEnemy();
    }

    private void Move()
    {   
        var h = CanvasManager.manager.JoystickUI.Value.x;
        var v = CanvasManager.manager.JoystickUI.Value.y;

        var velocity = new Vector3(h, 0, v);
        velocity = transform.TransformDirection(velocity) * moveSpeed;

        if (!cc.isGrounded) yVelocity -= 9.8f * gravityScale * Time.deltaTime;
        else yVelocity = 0;

        velocity.y = yVelocity;

        cc.Move(velocity * Time.deltaTime);
    }

    private void CameraMove()
    {
        var mouseX = CanvasManager.manager.DragUI.Delta.x;
        var mouseY = CanvasManager.manager.DragUI.Delta.y;

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
            hit.collider.GetComponent<IInteractable>().DisplayUI();
    }

    private void KillEnemy()
    {
        if (!GameManager.manager.IsStartGame) killAmount = 0;

        if (curShootTime > 0)
        {
            curShootTime -= Time.deltaTime;
        }
        else
        {
            RaycastHit[] hits = Physics.RaycastAll(mainCam.transform.position, mainCam.transform.forward, 10000, ~LayerMask.GetMask("Player"));
            hits = hits.OrderBy((item) => Vector3.Distance(mainCam.transform.position, item.point)).ToArray();

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<Enemy>().Damage(10);
                    gunAnim.SetTrigger("shot");
                }
                else
                {
                    if(hit.collider.gameObject.layer != LayerMask.GetMask("Ignore Raycast"))
                        break;
                }
            }

            curShootTime = shootTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(mainCam.transform.position, mainCam.transform.position + mainCam.transform.forward * interactDist);
    }
}
