using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitCard : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private UnitData targetData;
    private int index;
    private Vector2 pos;
    private Vector3 spawnPos;
    private bool isDragging;
    private GameObject previewObject;

    public bool IsDragging => isDragging;

    public void Init(int index, Vector2 pos, UnitData data, Sprite sprite)
    {
        this.index = index;
        this.pos = pos;
        this.targetData = data;
        GetComponent<Image>().sprite = sprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!isDragging) return;
        transform.position = eventData.position;

        if (CheckGrid(out var hitinfo)) spawnPos = hitinfo.collider.transform.position;
        else
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit);
            spawnPos = new Vector3(hit.point.x, 0, hit.point.z);
        }
        previewObject.transform.position = new Vector3(spawnPos.x, 0, spawnPos.z);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.position = eventData.position;
        UnitSetManager.manager.ActiveMenu(false);
        GridGroup.group.ActiveGrid(targetData.SetableGrid);

        if (CheckGrid(out var hitinfo)) spawnPos = hitinfo.collider.transform.position;
        previewObject = Instantiate(targetData.Prefeb, new Vector3(spawnPos.x, 0, spawnPos.z), Quaternion.identity);
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!isDragging) return;

        isDragging = false;

        if (CheckGrid(out var hitinfo))
        {
            if(UnitSetManager.manager.UseCard(index, hitinfo.collider.GetComponent<GridPlane>().YXIndex, spawnPos))
                transform.localPosition = pos;
        }
        UnitSetManager.manager.ActiveMenu(true);
        GridGroup.group.DisableAllGrid();
        Destroy(previewObject);
    }

    private bool CheckGrid(out RaycastHit hitinfo)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hitinfo, Mathf.Infinity, LayerMask.GetMask("Grid"));
    }

    private void Update()
    {
        if (!isDragging)
            transform.localPosition = Vector2.Lerp(transform.localPosition, pos, Time.deltaTime * 10);
    }
}
