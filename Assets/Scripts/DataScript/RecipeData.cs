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
    public bool CheckEnoughInput(SerializedDictionary<ProductData, int> inputs)
    {
        foreach (var (product, count) in inputProducts)
        {
            if (!inputs.TryGetValue(product, out var ic) || ic < count)
            {
                return false;
            }
        }
        return true;
    }
    public int ProduceProducts(ref SerializedDictionary<ProductData, int> inputs)
    {
        if (CheckEnoughInput(inputs))
        {
            foreach (var (product, count) in inputProducts)
            {
                inputs[product] -= count;
            }
            if (inputs.TryGetValue(outputProduct, out var currentCount))
            {
                inputs[outputProduct] = currentCount + outputCount;
            }
            else
            {
                inputs[outputProduct] = outputCount;
            }
            return outputCount;
        }
        return 0;
    }
}
