using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageDynamicTooltip : MonoBehaviour
{
    [SerializeField] StorageScript storage;
    [SerializeField] SimpleTooltip tooltip;
    [SerializeField] Interpreter i;
    public void OnStorageUpdate()
    {
        tooltip.infoLeft = "";
        tooltip.infoRight = "";
        Dictionary<ProductData, int> inGameStorage = storage.Storage();
        foreach (var product in i.GetProductKind(ProductVariableKind.ALL_PRODUCTS))
        {
            inGameStorage.TryGetValue(product, out int value);
            tooltip.infoLeft += $"{product.ProductName}: {value}\n";
            tooltip.infoRight += $"${product.ProductPrice} each\n";
        }
    }
}
