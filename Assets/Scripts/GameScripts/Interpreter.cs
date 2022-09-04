using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Toolbox.Editor;
using UnityEngine.Events;
using System.Linq;

public class Interpreter : BE2_TargetObject
{
    [SerializeField] private SerializedDictionary<ProductVariableKind, ProductData[]> productDictionary = new();

    public class UEvent_List_Var : UnityEvent<List<string>> { }
    public UEvent_List_Var OnVariableUpdate = new();

    public ProductData[] GetProductKind(ProductVariableKind kind)
    {
        if (productDictionary.TryGetValue(kind, out var products))
        {
            return products;
        }
        return new ProductData[0];
    }

}

public enum ProductVariableKind
{
    CROP, ANIMAL, RECIPE
}