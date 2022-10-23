using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isBeingBlocked = false;
    [SerializeField] Vector3 CameraOffset = Vector3.forward * 10;
    private void OnMouseDown()
    {
        originalPosition = this.transform.position;

    }
    private void OnMouseDrag()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
    }
    private void OnMouseUp()
    {
        if (isBeingBlocked)
        {
            this.transform.position = originalPosition;
        }
    }
}
