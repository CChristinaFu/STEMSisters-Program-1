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
        * Sell to market, will total all the prices for the products in storage, 
            and add that to the money system; also clears storage at the end
    */
    [SerializeField] private SerializedDictionary<ProductData, int> storage;
    [SerializeField] List<RecipeData> recipes = new();
    [SerializeField] MoneySystem market;
    private void Awake()
    {
        if (market == null)
        {
            market = FindObjectOfType<MoneySystem>();

        }
    }
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
    public void ClearStorage()
    {
        storage.Clear();

    }
    public int SellToMarket()
    {
        int total = 0;
        foreach (var (product, count) in storage)
        {
            total += product.productPrice * count;
        }
        ClearStorage();
        market.UpdateMoney(total);
        return total;
    }
#if UNITY_EDITOR
    [SerializeField, EditorButton(nameof(TestHarvest))] private FieldSystem currentTestingField;
    [SerializeField, EditorButton(nameof(TestCreateNewProduct)), EditorButton(nameof(TestSellStorage))] private int testRecipeID = -1;

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
    private void TestSellStorage()
    {
        var totalPrice = SellToMarket();
        Debug.Log($"total money made = {totalPrice}");
    }
#endif
}
