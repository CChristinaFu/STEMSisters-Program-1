using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownVariableAssign : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    public void UpdateDropDown(List<string> VarName)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(VarName);
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    private void OnEnable()
    {
        if (FindObjectOfType<Interpreter>() is Interpreter I)
        {
            I.OnVariableUpdate.AddListener(UpdateDropDown);

        }
    }
    private void OnDisable()
    {
        if (FindObjectOfType<Interpreter>() is Interpreter I)
        {
            I.OnVariableUpdate.RemoveListener(UpdateDropDown);

        }
    }
}
