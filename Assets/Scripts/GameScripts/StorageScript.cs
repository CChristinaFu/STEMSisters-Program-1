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
    [SerializeField] private SerializedDictionary<string, (ProductData, int)> storage;
    public Dictionary<string, (ProductData, int)> Storage() => storage.BuildNativeDictionary();
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

    public void AddOrUpdateStorage(ProductData product, int amount = 1)
    {
        if (storage.TryGetValue(product.Name, out var value))
        {
            storage[product.Name] = (value.Item1, value.Item2 + amount);
        }
        else
        {
            storage[product.Name] = (product, amount);
        }
    }

    private void StorageHasUpdated()
    {
        if (storage.Count == 0)
        {
            OnStorageUpdate.Invoke("Empty");
        }
        else
        {
            string storageString = StorageStringify();
            OnStorageUpdate.Invoke(storageString);
        }
    }

    public string StorageStringify()
    {
        string storageString = "{\n";
        foreach (var key in storage.Keys)
        {
            var item = storage[key].Item1;
            var amount = storage[key].Item2;
            storageString += $"\t{item.ProductName} (amount = {amount}, price = {item.ProductPrice}),\n";

        }
        storageString += "}";
        return storageString;
    }

    public bool HarvestCropFromField(FieldSystem field)
    {
        if (field.TryHarvestFirstAvailableCrop(out var crop))
        {
            AddOrUpdateStorage(crop, 1);
            // if (storage.ContainsKey(crop))
            // {
            //     storage[crop] += 1;
            // }
            // else
            // {
            //     storage[crop] = 1;
            // }
            StorageHasUpdated();
            return true;
        }
        return false;
    }

    public bool MoveToStorage(FieldSystem field)
    {
        bool hasHarvested = false;

        foreach (CropData crop in field.HarvestAllCrops())
        {
            AddOrUpdateStorage(crop, 1);
            // if (storage.ContainsKey(crop))
            // {
            //     storage[crop] += 1;
            // }
            // else
            // {
            //     storage[crop] = 1;
            // }
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
            recipes[recipeID].ProduceProducts(ref storage);
            StorageHasUpdated();
            return true;
        }
        return false;
    }
    public bool CreateNewProduct(RecipeData recipe)
    {
        int produced = recipe.ProduceProducts(ref storage);
        StorageHasUpdated();
        Debug.Log($"{storage.ContainsKey(recipe.outputProduct.Name)}");
        return produced > 0;
    }
    public SellResult SellProduct(ProductData product)
    {
        SellResult result = SellResult.NOT_SOLD;
        Debug.LogWarning($"Try sell {product.Name}, {storage.ContainsKey(product.Name)}");
        if (storage.TryGetValue(product.Name, out var item))
        {
            if (item.Item2 == 1)
            {
                storage.Remove(product.Name);
                market.UpdateMoney(product.ProductPrice);
                result = SellResult.SOLD_LAST;
            }
            else if (item.Item2 > 1)
            {
                storage[product.Name] = (item.Item1, item.Item2 - 1);
                market.UpdateMoney(product.ProductPrice);
                result = SellResult.SOLD_ONE;
            }
            else
            {
                storage.Remove(product.Name);
                result = SellResult.REMOVED_EMPTY;
            }
        }
        StorageHasUpdated();
        print(result.ToString());
        return result;
    }

    public void ClearStorage()
    {
        storage.Clear();
        StorageHasUpdated();
    }
    /*    public int SellToMarket()
        {
            int total = 0;
            foreach (var (product, count) in storage)
            {
                total += product.ProductPrice * count;
            }
            ClearStorage();
            market.UpdateMoney(total);
            return total;
        }
    */
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
        //var totalPrice = SellToMarket();
        //Debug.Log($"total money made = {totalPrice}");
        Debug.LogError("NO LONGER in use");
    }
    // #endif
}

public enum SellResult
{
    SOLD_ONE,
    SOLD_LAST,
    REMOVED_EMPTY,
    NOT_SOLD,
}