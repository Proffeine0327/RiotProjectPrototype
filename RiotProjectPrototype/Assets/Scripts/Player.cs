using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerUpgrade { IncreaseBulletSpawnAmount, DecreaseBulletSpawnTime, RandomExplosion }

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
    [Header("Attack")]
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackTime;
    [SerializeField] private GameObject bulletPrefeb;
    [Header("Exp")]
    [SerializeField] private float exp;
    [SerializeField] private float lvlupexp;
    [SerializeField] private int lvl;
    [Header("Skill")]
    [SerializeField] private float rollCooltime;
    [SerializeField] private float rollDistance;
    [SerializeField] private bool canRoll;

    private Rigidbody rb;
    private float camRotationX;
    private float curAttackTime;

    public float MaxHp { get { return maxHp; } }
    public float Hp { get { return hp; } }

    public void Damage(float amount, Vector3 targetpos)
    {
        if (!isInvincible)
        {
            var pos = transform.position - targetpos;
            pos.y = 0;
            pos = pos.normalized;
            pos.y = 1.5f;

            rb.AddForce(pos * 200);
            hp -= amount;
        }
    }

    public void SetUpgrade(PlayerUpgrade upgrade)
    {
        Debug.Log(upgrade.ToString());
    }

    public void GetExp(float amount)
    {
        exp += amount;
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
        Attack();
        Skill();
        ExpControl();
    }

    private void Move()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var isGround = Physics.CheckSphere(transform.position + groundCastOffset, groundCastRadius, groundCastLayer);

        var dir = transform.TransformDirection(new Vector3(h, 0, v).normalized * moveSpeed);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dir *= 1.5f;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 70, Time.deltaTime * 10);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime * 10);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
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
        camRotationX = Mathf.Clamp(camRotationX, -10, 80);
        camPivot.localRotation = Quaternion.Euler(camRotationX, 0, 0);
    }

    private void Attack()
    {
        if (curAttackTime <= 0)
        {
            var enemys = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));
            var sortedbydistance = enemys.OrderBy(item => Vector3.Distance(item.transform.position, attackPoint.position)).ToList();

            if (sortedbydistance.Count > 0)
            {
                var bullet = Instantiate(bulletPrefeb, attackPoint.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().Init(damage, sortedbydistance[0].transform.position - attackPoint.position);

                curAttackTime = attackTime;
            }
        }
        else
        {
            curAttackTime -= Time.deltaTime;
        }
    }

    private void ExpControl()
    {
        if (exp >= lvlupexp)
        {
            exp -= lvlupexp;
            lvl++;
            CanvasManager.manager.UpgradeUI.DisplayUpgradeUI();
        }
    }

    private void Skill()
    {
        if(Input.GetKeyDown(KeyCode.C) && canRoll)
        {
            canRoll = false;
            StartCoroutine(RollSkill());
        }
    }

    private IEnumerator RollSkill()
    {
        for(float t = 0; t < 0.25f; t += Time.deltaTime)
        {
            if(!Physics.CheckSphere(transform.position + transform.forward * 0.75f, 0.1f))
                transform.Translate(transform.forward * rollDistance * Time.deltaTime / 0.25f, Space.World);
            yield return null;
        }

        yield return new WaitForSeconds(rollCooltime);

        canRoll = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + groundCastOffset, groundCastRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 0.75f, 0.1f);
    }
}
