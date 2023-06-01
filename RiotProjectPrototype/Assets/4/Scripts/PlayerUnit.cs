using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [SerializeField] private UnitData data;
    [SerializeField] private GameObject rangeObject;
    [SerializeField] private GameObject bulletPrefeb;

    private bool isDragging;
    private Vector3 beforepos;
    private Vector2Int beforeindex;
    private int lvl = 1;

    public UnitData Data => data;
    public int Lvl => lvl;

    public void Init(UnitData data)
    {
        this.data = data;
        rangeObject.SetActive(false);
    }

    public void LevelUp() => lvl++;

    private void Start()
    {
        rangeObject.transform.localScale = new Vector3(data.AttackRange * 2 / transform.lossyScale.x, 0.005f, data.AttackRange * 2 / transform.lossyScale.x);
    }

    private void OnMouseDown()
    {
        beforepos = transform.position;
        GridGroup.group.ActiveGrid(data.SetableGrid);
        rangeObject.SetActive(true);
        Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity, LayerMask.GetMask("Grid"));
        beforeindex = hit.collider.GetComponent<GridPlane>().YXIndex;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity, LayerMask.GetMask("Grid"))) transform.position = new Vector3(hit.collider.transform.position.x, 0, hit.collider.transform.position.z);
        else
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit2);
            transform.position = new Vector3(hit2.point.x, 0, hit2.point.z);
        }
    }

    private void OnMouseUp()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity, LayerMask.GetMask("Grid")))
        {
            if (!UnitSetManager.manager.MoveUnit(beforeindex, hit.collider.GetComponent<GridPlane>().YXIndex)) transform.position = beforepos;
        }
        else transform.position = beforepos;

        rangeObject.SetActive(false);
        GridGroup.group.DisableAllGrid();

        isDragging = false;
    }
}
