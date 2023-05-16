using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace first
{
    public class JoystickUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform bg;

        private Vector2 value;
        private float radius;
        private RectTransform rt;

        public Vector2 Value { get { return value; } }

        private void Update()
        {
            if (!GameManager.manager.IsStartGame)
            {
                bg.GetComponent<Image>().enabled = true;
                GetComponent<Image>().enabled = true;
            }
            else
            {
                bg.GetComponent<Image>().enabled = false;
                GetComponent<Image>().enabled = false;
            }
        }

        private void Start()
        {
            rt = transform as RectTransform;

            radius = bg.rect.width * 0.5f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos = eventData.position - (Vector2)bg.position;
            pos.x *= 1920f / Screen.width;
            pos.y *= 1080f / Screen.height;
            pos = Vector2.ClampMagnitude(pos, radius);

            value = pos / radius;
            rt.localPosition = pos;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 pos = eventData.position - (Vector2)bg.position;
            pos.x *= 1920f / Screen.width;
            pos.y *= 1080f / Screen.height;
            pos = Vector2.ClampMagnitude(pos, radius);

            value = pos / radius;
            rt.localPosition = pos;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            rt.localPosition = Vector3.zero;
            value = Vector2.zero;
        }
    }
}
