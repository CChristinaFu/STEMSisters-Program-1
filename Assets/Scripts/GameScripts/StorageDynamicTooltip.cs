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
        var inGameStorage = storage.Storage();
        foreach (var product in i.GetProductKind(ProductVariableKind.ALL_PRODUCTS))
        {
            ProductInfo value = (null, 0);
            inGameStorage.TryGetValue(product.Name, out value);
            tooltip.infoLeft += $"{product.ProductName}: {value.amount}\n";
            //tooltip.infoRight += $"${product.ProductPrice} each\n";
        }
    }
    public void OnMarketUpdate()
    {
        tooltip.infoLeft = "";
        //tooltip.infoRight = "";
        var inGameStorage = storage.Storage();
        foreach (var product in i.GetProductKind(ProductVariableKind.ALL_PRODUCTS))
        {
            ProductInfo value = (null, 0);
            inGameStorage.TryGetValue(product.Name, out value);
            tooltip.infoLeft += $"{product.ProductName} -  ${product.ProductPrice}\n";
            //tooltip.infoRight += $"${product.ProductPrice} each\n";
        }

    }

}
