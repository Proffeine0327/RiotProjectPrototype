using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Vector2 clampZPos;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 0, Input.mouseScrollDelta.y), Space.World);
        transform.position = new Vector3(startPos.x, startPos.y, Mathf.Clamp(transform.position.z, clampZPos.x, clampZPos.y));
    }
}
