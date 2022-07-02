using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IPointerDownHandler, IDropHandler
{
    private Vector3 startPosition;
    private Vector3 difPosition;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject panelGhost;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - difPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        float sizeY = GetComponent<RectTransform>().rect.size.y;
        //to do - reorder elements
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = transform.position;
        difPosition = Input.mousePosition - startPosition;
        EventSystem.current.SetSelectedGameObject(gameObject);
        EventSystem.current.currentSelectedGameObject.transform.SetParent(canvas.transform);
        EventSystem.current.currentSelectedGameObject.transform.SetAsLastSibling();
        //to do - check if first or last sibling works
        Debug.Log($"Started dragging: {gameObject.name}");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
