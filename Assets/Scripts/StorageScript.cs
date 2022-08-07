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
    [SerializeField] List<RecipeData> recipes = new();

    public bool MoveToStorage(FieldSystem field)
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
    public bool CreateNewProduct(int recipeID)
    {
        if (recipeID >= 0 && recipeID < recipes.Count)
        {
            recipes[recipeID].ProduceProducts(storage);
            return true;
        }
        return false;
    }

#if UNITY_EDITOR
    [SerializeField, EditorButton(nameof(TestHarvest))] private FieldSystem currentTestingField;
    [SerializeField, EditorButton(nameof(TestCreateNewProduct))] private int testRecipeID = -1;

    private void TestHarvest()
    {
        if (currentTestingField)
        {
            MoveToStorage(currentTestingField);
        }
    }

    private void TestCreateNewProduct()
    {
        CreateNewProduct(testRecipeID);
    }
#endif
}
