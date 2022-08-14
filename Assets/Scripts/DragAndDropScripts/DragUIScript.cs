using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DragUIScript : MonoBehaviour,
IDragHandler, IPointerDownHandler,
IEndDragHandler, IPointerClickHandler, IBeginDragHandler
{
    private Vector3 startPosition;
    private Vector3 difPosition;
    [SerializeField] GameObject codeEditor;
    [SerializeField] GameObject panelGhost;
    public UnityEvent OnStartDragging = new();

    private void Start()
    {
        if (codeEditor == null)
        {
            codeEditor = GameObject.FindGameObjectWithTag(UtilityConstAndExt.CODE_EDITOR_TAG);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - difPosition;
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
        EventSystem.current.currentSelectedGameObject.transform.SetParent(codeEditor.transform);
        EventSystem.current.currentSelectedGameObject.transform.SetAsLastSibling();
        Debug.Log($"Started dragging: {gameObject.name}");
        OnStartDragging.Invoke();
    }
}
