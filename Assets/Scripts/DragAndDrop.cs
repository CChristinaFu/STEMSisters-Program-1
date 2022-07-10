using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, 
IDragHandler, IPointerDownHandler, IDropHandler, 
IEndDragHandler, IPointerClickHandler, IBeginDragHandler
{
    private Vector3 startPosition;
    private Vector3 difPosition;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject panelGhost;

    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - difPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        EventSystem.current.currentSelectedGameObject.transform.parent = transform;
        float sizeY = GetComponent<RectTransform>().rect.size.y;
        int index = (int)((((transform.position.y+(sizeY/2)) - Input.mousePosition.y))/35)-1;
        if(index < 0)
        {
            index = 0;
        }
        Debug.Log(index);
        EventSystem.current.currentSelectedGameObject.transform.SetSiblingIndex(index);
        EventSystem.current.SetSelectedGameObject(null);
        Debug.Log("drop " + gameObject.name);
        Debug.Log(transform);
        // rebuilds the layout and its child elements (previously done in UIDrag)
        // the objects that allow drop are the ones who actually need this rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = transform.position;
        difPosition = Input.mousePosition - startPosition;
        EventSystem.current.SetSelectedGameObject(gameObject);
        EventSystem.current.currentSelectedGameObject.transform.SetParent(canvas.transform);
        EventSystem.current.currentSelectedGameObject.transform.SetAsLastSibling();
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
