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

    public void Init(int index, Vector2 pos, UnitData data)
    {
        this.index = index;
        this.pos = pos;
        this.targetData = data;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        var ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, LayerMask.GetMask("Grid"))) spawnPos = hitinfo.collider.transform.position;
        previewObject.transform.position = new Vector3(spawnPos.x, 0, spawnPos.z);
        isDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.position = eventData.position;
        UnitSetManager.manager.DragCard(targetData.Type);
        UnitSetManager.manager.ActiveMenu(false);

        var ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, LayerMask.GetMask("Grid"))) spawnPos = hitinfo.collider.transform.position;
        previewObject = Instantiate(targetData.Prefeb, new Vector3(spawnPos.x, 0, spawnPos.z), Quaternion.identity);
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        UnitSetManager.manager.ActiveMenu(true);

        var ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, LayerMask.GetMask("Grid")))
        {
            if (targetData.Type == UnitType.normal)
            {
                if (hitinfo.collider.CompareTag("GroundGrid"))
                {
                    UnitSetManager.manager.UseCard(index, spawnPos);
                    transform.localPosition = pos;
                }
            }

            if (targetData.Type == UnitType.trap)
            {
                if (hitinfo.collider.CompareTag("RoadGrid"))
                {
                    UnitSetManager.manager.UseCard(index, spawnPos);
                    transform.localPosition = pos;
                }
            }
        }
        SetGridGroup.group.ActiveGrid(false);
        Destroy(previewObject);
    }

    private void Update()
    {
        if(!isDragging)
            transform.localPosition = Vector2.Lerp(transform.localPosition, pos, Time.deltaTime * 10);
    }
}
