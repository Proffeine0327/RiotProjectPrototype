using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace first
{
    public class DragUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public Vector2 delta;
        private bool isDragging;

        public Vector2 Delta => delta;

        private void Update()
        {
            if (!isDragging) delta = Vector2.zero;
            else delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        public void OnPointerDown(PointerEventData eventData) => isDragging = true;
        public void OnPointerUp(PointerEventData eventData) => isDragging = false;
        public void OnPointerExit(PointerEventData eventData) => isDragging = false;
    }
}
