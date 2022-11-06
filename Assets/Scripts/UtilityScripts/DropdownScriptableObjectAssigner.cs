using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DropdownScriptableObjectAssigner : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] ProductVariableKind varKind;

    public void Initialize(ProductVariableKind kind, Dropdown item)
    {
        varKind = kind;
        dropdown = item;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<Dropdown>();
        }
    }

    void Start()
    {
        if (FindObjectOfType<Interpreter>() is Interpreter I)
        {
            UpdateDropDown(I);
        }
        if (GetComponent<BE2_DropdownDynamicResize>() is BE2_DropdownDynamicResize Resize)
        {
            Resize.Resize();
        }
    }

    private void UpdateDropDown(Interpreter I)
    {
        // For each product we get based on kind, extract name and image and produce a new dropdown option
        dropdown.ClearOptions();
        dropdown.AddOptions(I.GetProductKind(varKind).Select(product => new Dropdown.OptionData(product.Name, product.ProductImage)).ToList());
        dropdown.value = 0;
        // print(dropdown.options[dropdown.value].text);
        dropdown.RefreshShownValue();
    }
}
