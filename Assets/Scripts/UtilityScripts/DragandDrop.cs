using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private Vector3 originalPosition;
    private int numberOfBlockingObjects = 0;
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
        if (numberOfBlockingObjects > 0)
        {
            this.transform.position = originalPosition;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        numberOfBlockingObjects++;
        Debug.LogWarning(numberOfBlockingObjects);
    }
    private void OnCollisionExit(Collision other)
    {
        numberOfBlockingObjects--;
        Debug.LogWarning(numberOfBlockingObjects);

    }
}
