using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private bool isDragging;
    [SerializeField] private Vector2 value;

    private float radius;

    private RectTransform bg;
    private RectTransform rt;

    public Vector2 Value { get { return value; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

        Vector2 pos = eventData.position - (Vector2)bg.position;
        pos.x *= 1920f / Screen.width;
        pos.y *= 1080f / Screen.height;
        pos = Vector2.ClampMagnitude(pos, radius);

        value = pos / radius;
        rt.localPosition = pos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        rt.localPosition = Vector3.zero;
        value = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!isDragging) return;

        Vector2 pos = eventData.position - (Vector2)bg.position;
        pos.x *= 1920f / Screen.width;
        pos.y *= 1080f / Screen.height;
        pos = Vector2.ClampMagnitude(pos, radius);

        value = pos / radius;
        rt.localPosition = pos;
    }

    private void Start()
    {
        rt = transform as RectTransform;
        bg = transform.parent as RectTransform;

        radius = bg.rect.width * 0.5f;
    }
}
