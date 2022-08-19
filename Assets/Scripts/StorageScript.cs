using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UEvent_str OnStorageUpdate = new();
    private void Start()
    {
        if (market == null)
        {
            market = FindObjectOfType<MoneySystem>();
        }
        StorageHasUpdated();
    }

    private void StorageHasUpdated()
    {
        if (storage.Count == 0)
        {
            OnStorageUpdate.Invoke("Empty");
        }
        else
        {
            string storageString = "{\n";
            foreach (var item in storage.Keys)
            {
                storageString += $"\t{item.productName} (amount = {storage[item]}, price = {item.productPrice}),\n";
            }
            storageString += "}";
            OnStorageUpdate.Invoke(storageString);
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
        if (hasHarvested)
        {
            StorageHasUpdated();
        }
        return hasHarvested;
    }

    public void MoveToStorageNoOutput(FieldSystem field)
    {
        MoveToStorage(field);
    }

    public bool CreateNewProduct(int recipeID)
    {
        if (recipeID >= 0 && recipeID < recipes.Count)
        {
            recipes[recipeID].ProduceProducts(storage);
            StorageHasUpdated();
            return true;
        }
        return false;
    }
    public void ClearStorage()
    {
        storage.Clear();
        StorageHasUpdated();
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
    // #if UNITY_EDITOR
    [SerializeField, EditorButton(nameof(TestHarvest))] private FieldSystem currentTestingField;
    [SerializeField, EditorButton(nameof(TestCreateNewProduct)), EditorButton(nameof(TestSellStorage))] private int testRecipeID = -1;

    private void TestHarvest()
    {
        if (currentTestingField)
        {
            MoveToStorage(currentTestingField);
        }
    }

    public void TestCreateNewProduct()
    {
        CreateNewProduct(testRecipeID);
    }
    public void TestSellStorage()
    {
        var totalPrice = SellToMarket();
        Debug.Log($"total money made = {totalPrice}");
    }
    // #endif
}
