using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace third
{

    public class PlayerUnitDragManager : MonoBehaviour
    {
        private bool isDraggingUnit;
        private PlayerUnit dragUnit;

        private void Update()
        {
            Dragging();
        }

        private void Dragging()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("PlayerUnit") && !hit.collider.GetComponent<PlayerUnit>().IsOnAttackField)
                    {
                        isDraggingUnit = true;
                        dragUnit = hit.collider.GetComponent<PlayerUnit>();
                    }
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (isDraggingUnit)
                {
                    RaycastHit hit;
                    Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));

                    if (hit.collider != null)
                    {
                        dragUnit.transform.position = new Vector3(hit.point.x, 1, Mathf.Clamp(hit.point.z, -10, -1));
                        dragUnit.Dragging(true);
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (isDraggingUnit)
                {
                    dragUnit.Dragging(false);
                    isDraggingUnit = false;
                    dragUnit = null;
                }
            }
        }
    }
}