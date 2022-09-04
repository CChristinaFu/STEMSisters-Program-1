using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "STEMSisters Program 1/RecipeData", order = 0)]
public class RecipeData : ScriptableObject
{
    public SerializedDictionary<ProductData, int> inputProducts = new();
    public ProductData outputProduct;
    public int outputCount = 1;
    public bool checkEnoughInput(SerializedDictionary<ProductData, int> inputs)
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
    public int ProduceProducts(SerializedDictionary<ProductData, int> inputs)
    {
        if (checkEnoughInput(inputs))
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
