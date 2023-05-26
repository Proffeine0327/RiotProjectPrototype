using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected float maxHp;

    protected float curHp;

    public float CurHp => curHp;
    public float MaxHp => maxHp;
    
    public void Damage(float amount)
    {
        curHp -= amount;
    }

    protected virtual void Start()
    {
        curHp = maxHp;
        CanvasManager.manager.HpBarUIManager.MakeHpBar(this);
    }

    protected virtual void Update()
    {
        if(curHp <= 0) Destroy(gameObject);
    }
}