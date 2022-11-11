using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// v2.7 - added a class to the extras that implements the logic for show/hide the Blocks Selection panel  
public class BE2_HideBlocksSelection : MonoBehaviour
{
    BE2_Canvas _blocksSelectionCanvas;
    public Vector3 hidePosition;
    Dictionary<Transform, Vector3> _envs = new Dictionary<Transform, Vector3>();

    void Start()
    {
        _blocksSelectionCanvas = GetComponentInParent<BE2_Canvas>();
        hidePosition = _blocksSelectionCanvas.transform.GetChild(0).position;

        GetComponent<Button>().onClick.AddListener(HideBlocksSelection);

        foreach (BE2_UI_SelectionButton button in FindObjectsOfType<BE2_UI_SelectionButton>())
        {
            button.GetComponent<Button>().onClick.AddListener(ShowBlocksSelection);
        }

        foreach (I_BE2_ProgrammingEnv env in BE2_ExecutionManager.Instance.ProgrammingEnvsList)
        {
            //Debug.Log(env);
            //Debug.Log(env.Transform.position);
            // Debug.Log(env.Transform.GetComponentInParent<BE2_Canvas>()); //error occurs here
            //Debug.Log(env.Transform.GetComponentInParent<BE2_Canvas>().Canvas);  
            // Debug.Log(env.Transform.GetComponentInParent<BE2_Canvas>().Canvas.transform.GetChild(0));
            if (env.Transform.GetComponentInParent<BE2_Canvas>() is BE2_Canvas envCanvas)
            {
                _envs.Add(envCanvas.Canvas.transform.GetChild(0), env.Transform.position);
            }
        }
    }

    // void Update()
    // {

    // }

    public void HideBlocksSelection()
    {
        _blocksSelectionCanvas.gameObject.SetActive(false);

        foreach (KeyValuePair<Transform, Vector3> env in _envs)
        {
            env.Key.position = hidePosition;
        }
    }

    public void ShowBlocksSelection()
    {
        if (!_blocksSelectionCanvas.gameObject.activeSelf)
        {
            _blocksSelectionCanvas.gameObject.SetActive(true);

            foreach (KeyValuePair<Transform, Vector3> env in _envs)
            {
                env.Key.position = env.Value;
            }
        }
    }

    public void ToggleBlockSelection()
    {
        if (_blocksSelectionCanvas.gameObject.activeSelf)
        {
            HideBlocksSelection();
        }
        else
        {
            ShowBlocksSelection();
        }
    }
}
