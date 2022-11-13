using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "STEMSisters_Program_1/RecipeData", order = 0)]
public class RecipeData : ProductData
{
    public SerializedDictionary<ProductData, int> inputProducts = new();
    public ProductData outputProduct;
    public override Sprite ProductImage { get => outputProduct.ProductImage; protected set { } }
    public override string ProductName { get => outputProduct.ProductName; protected set { } }
    public override int ProductPrice { get => outputProduct.ProductPrice; protected set { } }
    public int outputCount = 1;
    public bool CheckEnoughInput(SerializedDictionary<string, ProductInfo> inputs)
    {
        foreach (var (product, count) in inputProducts)
        {
            if (!inputs.TryGetValue(product.Name, out var ic) || ic.amount < count)
            {
                return false;
            }
        }
        return true;
    }
    public int ProduceProducts(ref SerializedDictionary<string, ProductInfo> inputs)
    {
        if (CheckEnoughInput(inputs))
        {
            foreach (var (product, count) in inputProducts)
            {
                inputs[product.Name] = (inputs[product.Name].product, inputs[product.Name].amount - count);
            }
            if (inputs.TryGetValue(outputProduct.Name, out var current))
            {
                inputs[outputProduct.Name] = (current.product, current.amount + outputCount);
            }
            else
            {
                inputs[outputProduct.Name] = (outputProduct, outputCount);
            }
            return outputCount;
        }
        return 0;
    }
}
