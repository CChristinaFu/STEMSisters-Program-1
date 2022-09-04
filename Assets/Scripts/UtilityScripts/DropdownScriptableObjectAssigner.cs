using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DropdownScriptableObjectAssigner : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] ProductVariableKind varKind;

    public void Initialize(ProductVariableKind kind)
    {
        varKind = kind;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<Dropdown>();
        }
    }

    private void OnEnable()
    {
        if (FindObjectOfType<Interpreter>() is Interpreter I)
        {
            dropdown.AddOptions(I.GetProductKind(varKind).Select(product => product.productName).ToList());
        }
    }
}
