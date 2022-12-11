using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMover : MonoBehaviour
{
    [SerializeField] RectTransform panel;
    [SerializeField] bool usePositions;
    [SerializeField] bool showOnStart;
    [SerializeField, HideIf(nameof(usePositions), false)] Vector3 ActivePosition;
    [SerializeField, HideIf(nameof(usePositions), false)] Vector3 HiddenPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (panel == null)
        {
            panel = GetComponent<RectTransform>();

        }
        if (showOnStart)
        {
            ShowPanel();
        }
        else
        {
            HidePanel();
        }
    }
    public void ShowPanel()
    {
        if (usePositions)
        {
            panel.anchoredPosition3D = ActivePosition;
        }
        else if (panel.GetComponent<Canvas>() is Canvas c)
        {
            c.targetDisplay = 0;
        }
    }
    public void HidePanel()
    {
        if (usePositions)
        {
            panel.anchoredPosition3D = HiddenPosition;
        }
        else if (panel.GetComponent<Canvas>() is Canvas c)
        {
            c.targetDisplay = 2;
        }
    }
}
