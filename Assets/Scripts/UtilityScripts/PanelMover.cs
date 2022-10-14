using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMover : MonoBehaviour
{
    [SerializeField] Canvas panel;
    [SerializeField] Vector3 ActivePosition;
    [SerializeField] Vector3 HiddenPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (panel == null)
        {
            panel = GetComponent<Canvas>();

        }
    }
    public void ShowPanel()
    {
        panel.targetDisplay = 0;
    }
    public void HidePanel()
    {
        panel.targetDisplay = 2;
    }
}
