using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DropUIScript : MonoBehaviour, IDropHandler
{
    public UnityEvent OnDropBlock = new();
    public void OnDrop(PointerEventData eventData)
    {
        // Ignore Top Block when trying to place inside
        if (EventSystem.current.currentSelectedGameObject.GetComponent<TopBlock>() != null) return;

        EventSystem.current.currentSelectedGameObject.transform.parent = transform;
        float sizeY = GetComponent<RectTransform>().rect.size.y;
        int index = (int)((((transform.position.y + (sizeY / 2)) - Input.mousePosition.y)) / 35) - 1;
        if (index < 0)
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
        OnDropBlock.Invoke();
    }
}
