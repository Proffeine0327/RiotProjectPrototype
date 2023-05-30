using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace second
{
    public class CardCell : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image img;
        [SerializeField] private Text txt;

        private RectTransform bg;
        private UnitDataTable unitdatas;
        private int cardIndex;
        private int unitId;
        private bool isSelect;
        private Vector3 spawnPos;
        private GameObject previewObject;
        private Material previewMaterial;

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;

            if (RectTransformUtility.RectangleContainsScreenPoint(bg, eventData.position))
            {
                img.enabled = true;
                txt.gameObject.SetActive(true);
                previewObject.SetActive(false);
            }
            else
            {
                RaycastHit hitinfo;
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitinfo);
                spawnPos = new Vector3(Mathf.RoundToInt(hitinfo.point.x), 1.5f, Mathf.RoundToInt(hitinfo.point.z));
                spawnPos = new Vector3(Mathf.Clamp(spawnPos.x, -20, -4), 1.5f, Mathf.Clamp(spawnPos.z, -7, 7));
                previewObject.transform.position = spawnPos;

                img.enabled = false;
                txt.gameObject.SetActive(false);
                previewObject.SetActive(true);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isSelect = true;
            transform.position = eventData.position;

            previewObject = new GameObject();
            previewObject.AddComponent<MeshFilter>().mesh = unitdatas.Units[unitId].Prefeb.GetComponent<MeshFilter>().sharedMesh;
            previewObject.AddComponent<MeshRenderer>().sharedMaterial = previewMaterial;
            previewObject.SetActive(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isSelect = false;
            if (!RectTransformUtility.RectangleContainsScreenPoint(bg, eventData.position))
            {
                InGameCardManager.manager.UseCard(cardIndex, spawnPos);
                img.enabled = true;
                txt.gameObject.SetActive(true);
            }
            Destroy(previewObject);
        }

        private void Update()
        {
            if (!isSelect)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(99.333f + cardIndex * 200, 0, 0), Time.deltaTime * 15f);
            }

            txt.text = unitdatas.Units[unitId].Name;
        }

        public void Init(RectTransform bg, UnitDataTable unitdatas, Material previewMaterial)
        {
            this.bg = bg;
            this.unitdatas = unitdatas;
            this.previewMaterial = previewMaterial;
        }
        public void UpdateState(int unitId, int cardIndex)
        {
            this.unitId = unitId;
            this.cardIndex = cardIndex;
        }
    }
}