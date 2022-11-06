using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isBeingBlocked = false;
    private Vector3 offsetPos;
    [SerializeField] BoxCollider col;
    // [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 cameraOffset = Vector3.forward * 10;
    [SerializeField] Camera mainCamera = null;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        // if (rb == null)
        // {
        //     rb = GetComponent<Rigidbody>();
        // }
        if (col == null)
        {
            col = GetComponent<BoxCollider>();
        }
    }

    public Vector3 GetMousePos()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition) + cameraOffset;
    }

    private void OnMouseDown()
    {
        originalPosition = this.transform.position;
        offsetPos = originalPosition - GetMousePos();
        // col.enabled = false;
    }
    private void OnMouseDrag()
    {
        // var tempCol = new Collider[2];
        // isBeingBlocked = Physics.OverlapBoxNonAlloc(col.center, col.size / 2 + cameraOffset, tempCol) > 1;
        // print($"{gameObject} => {isBeingBlocked}: {tempCol[0]} {tempCol[1]}");
        // if (rb)
        // {
        //     rb.MovePosition(GetMousePos() + offsetPos);
        // }
        // else
        // {
        //     this.transform.position = GetMousePos() + offsetPos;
        // }
        this.transform.position = GetMousePos() + offsetPos;
    }
    private void OnMouseUp()
    {
        var tempCol = new Collider[2];
        print($"{col.center}, {col.size / 2 + Vector3.forward * 100}");
        isBeingBlocked = Physics.OverlapBoxNonAlloc(col.center, col.size / 2 + Vector3.forward * 100, tempCol) > 1;
        print($"{gameObject} => {isBeingBlocked}: {tempCol[0]} {tempCol[1]}");
        if (isBeingBlocked)
        {
            this.transform.position = originalPosition;
        }
        // col.enabled = true;
    }
}
