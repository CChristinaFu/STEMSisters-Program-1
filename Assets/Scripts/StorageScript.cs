using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : MonoBehaviour
{
    /*
    Storage hold all the products that were harvested, created
        * Store collection as a dictionary, key = crop data, value = count
        * Get the products from a field (harvesting)
        * Create by taking raw ingredients and making new product 
            * how do we store the recipies?
        * Clear store, should happen at the end of every turn
    */
    [SerializeField] private SerializedDictionary<ProductData, int> storage;
    [SerializeField] List<RecipeData> recipes =new();

    public bool moveToStorage(FieldData field)
    {
        bool hasHarvested = false;

        foreach (CropData crop in field.HarvestAllCrops())
        {
            if (storage.ContainsKey(crop))
            {
                storage[crop] += 1;
            }
            else
            {
                storage[crop] = 1;
            }
            hasHarvested = true;
        }
        return hasHarvested;
    }
public bool createNewProduct(int recipeID){
    if (recipeID >=0 && recipeID<recipes.Count){
        recipes[recipeID].ProduceProducts(storage);
        return true;
    }
    return false;
}
}
