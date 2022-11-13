using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragandDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private bool isBeingBlocked = false;
    private Vector3 offsetPos;
    [SerializeField] BoxCollider2D col;
    // [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 cameraOffset = Vector3.forward * 10;
    [SerializeField] Camera mainCamera = null;

    private void Awake()
    {
        originalPosition = this.transform.position;
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
            col = GetComponentInChildren<BoxCollider2D>();
        }
    }

    public Vector3 GetMousePos() => mainCamera.ScreenToWorldPoint(Input.mousePosition) + cameraOffset;

    public Vector3 GetMouseWithOffset() => GetMousePos() + offsetPos;

    private void OnMouseDown()
    {
        DragStart();
    }

    private void DragStart()
    {
        originalPosition = this.transform.position;
        offsetPos = originalPosition - GetMousePos();
        // col.enabled = false;
    }

    private void OnMouseDrag()
    {
        DragContinue();
    }

    private void DragContinue()
    {
        this.transform.position = GetMouseWithOffset();
    }

    private void OnMouseUp()
    {
        DragEnd();
    }

    private void DragEnd()
    {
        var tempCol = new Collider2D[2];
        // print($"{col.offset}, {col.size}");
        isBeingBlocked = Physics2D.OverlapBoxNonAlloc(transform.position, col.size, 0, tempCol) > 1;
        // print($"{gameObject} => {isBeingBlocked}: {tempCol[0]} {tempCol[1]}");
        if (isBeingBlocked)
        {
            this.transform.position = originalPosition;
        }
        // col.enabled = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragStart();
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragContinue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEnd();
    }
}
