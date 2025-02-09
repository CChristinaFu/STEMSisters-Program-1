using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Toolbox.Editor;
using UnityEngine.Events;
using System.Linq;

public class Interpreter : BE2_TargetObject
{
    public const int SECONDS_IN_DAY = 4;
    private readonly InterpreterError? NO_ERROR = null;
    private Coroutine timerCoroutine = null;
    [Header("Helper Variables")]
    [SerializeField] private Vector3 fieldStartingPosition;
    [SerializeField] private Vector2 FieldSpacing = Vector2.one;
    [SerializeField] private int fieldColumns = 4;
    [SerializeField] private ProductData emptyProductData;
    [SerializeField] private SerializedDictionary<ProductVariableKind, ProductData[]> productDictionary = new();
    private float currentTargetTime = 0;
    [SerializeField] float waitTime = 1;
    [SerializeField] float timerLimit = 60;

    [Header("External References")]
    [SerializeField] private GameObject fieldPrefab;
    [field: SerializeField]
    public StorageScript Storage { get; private set; }
    [field: SerializeField]
    public MoneySystem Money { get; private set; }
    [field: SerializeField] public BE2_ProgrammingEnv Environ { get; private set; }

    [field: SerializeField]
    public Dictionary<string, FieldSystem> Fields { get; private set; } = new();
    public UEvent_int OnDayUpdate = new();
    public UEvent_int OnCountdownUpdate = new();
    private void OnEnable()
    {
        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnPlay, TryStartTimer);
        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnStop, StopTimer);
    }
    private void OnDisable()
    {
        BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnPlay, TryStartTimer);
        BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnStop, StopTimer);
    }
    public void ResetField()
    {
        foreach (var f in Fields.Values)
        {
            Destroy(f.gameObject);
        }

        Fields.Clear();
    }
    public void TryStartTimer()
    {
        //check sum of all block prices is less than or equal to current money
        int totalPrice = 0;
        Environ.UpdateBlocksList();

        foreach (var block in Environ.BlocksList)
        {
            foreach (var childblock in block.Transform.GetComponentsInChildren<I_BE2_Block>())
            {
                totalPrice += childblock.BlockPrice;
                //Debug.Log(childblock.Transform);
            }
        }
        //Debug.Log(totalPrice);
        if (Money.HasEnoughMoney(totalPrice))
        {
            Money.UpdateMoney(-totalPrice);
            if (timerCoroutine is null)
            {
                currentTargetTime = waitTime;
                timerCoroutine = StartCoroutine(RunTimer());
            }
        }
        else
        {
            InGameConsoleScript.LogMessage("Not enough money");
        }
    }
    public void StopTimer()
    {
        if (timerCoroutine is not null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
            foreach (var field in Fields.Values)
            {
                field.DiscardAll();
            }
            Storage.ClearStorage();
        }
    }
    private IEnumerator RunTimer()
    {
        for (float i = 0; i < timerLimit; i += waitTime)
        {
            while (currentTargetTime > 0)
            {
                yield return null;
                currentTargetTime -= Time.deltaTime;
            }
            DebugGrowAll();
            OnDayUpdate.Invoke((int)i / SECONDS_IN_DAY + 1);
            OnCountdownUpdate.Invoke(SECONDS_IN_DAY - (int)i % SECONDS_IN_DAY);
            currentTargetTime = waitTime;
        }
        Debug.Log("finish running timer");
    }
    public class UEvent_List_Var : UnityEvent<List<string>> { }
    public UEvent_List_Var OnVariableUpdate = new();

    public ProductData[] GetProductKind(ProductVariableKind kind, bool ignoreBlankProducts = true, bool useRecipeOutput = false)
    {
        if (kind == ProductVariableKind.ALL_PRODUCTS)
        {
            // Combine Crop, Animal, and Recipe Products into a single array
            return GetProductKind(ProductVariableKind.AGRICULTURAL, ignoreBlankProducts)
                .Concat(GetProductKind(ProductVariableKind.RECIPE, ignoreBlankProducts: true, useRecipeOutput: true))
                .ToArray();
        }
        else if (kind == ProductVariableKind.AGRICULTURAL)
        {
            // Combine Crop and Animal, Products into a single array
            return GetProductKind(ProductVariableKind.CROP, ignoreBlankProducts)
                .Concat(GetProductKind(ProductVariableKind.ANIMAL, ignoreBlankProducts: true))
                .ToArray();
        }
        else if (productDictionary.TryGetValue(kind, out var products))
        {
            if (kind == ProductVariableKind.RECIPE && useRecipeOutput)
            {
                products = products.Select(p => ((RecipeData)p).outputProduct).ToArray();
            }
            if (ignoreBlankProducts)
            {
                return products;
            }
            ProductData[] newArray = new ProductData[products.Length + 1];
            newArray[0] = emptyProductData;
            System.Array.Copy(products, 0, newArray, 1, products.Length);
            return newArray;
        }
        return new ProductData[0];
    }
    # region Variable Functions
    public bool CreateOrUpdateField(string fieldName, string cropName)
    {
        FieldSystem field;
        if (!Fields.TryGetValue(fieldName, out field))
        {
            // FIXME: Field should be placed by user
            int row = -System.Math.DivRem(Fields.Count, fieldColumns, out int column);
            Vector3 offset = new(FieldSpacing.x * column, FieldSpacing.y * row);
            GameObject newField = Instantiate(fieldPrefab, fieldStartingPosition + offset, Quaternion.identity);
            field = newField.GetComponent<FieldSystem>();
            field.fieldName = fieldName;
            Fields.Add(fieldName, field);
        }
        if (productDictionary.TryGetValue(ProductVariableKind.CROP, out var crops)
         && System.Array.Find(crops, (x) => x.ProductName == cropName) is CropData crop)
        {
            field.SetFieldCrop(crop);
            return true;
        }
        return false;

    }

    public FieldSystem GetField(string fieldName)
    {
        if (Fields.TryGetValue(fieldName, out var field))
        {
            return field;
        }
        return null;
    }

    public bool CreatePen(string penName, string animalName)
    {
        // TODO: Add a way to create new pens
        return false;
    }

    // public <PenSystem> GetPen(string penName) {}

    # endregion
    # region Actions Functions

    public InterpreterError? PlaceItemInList(CropData crop, string fieldName)
    {
        if (Fields.TryGetValue(fieldName, out var field))
        {
            if (field.CropData == null)
            {
                field.SetFieldCrop(crop);
            }
            if (field.CropData == crop)
            {
                if (field.PlantFirstAvailableSeed()) return NO_ERROR;
                else return new InterpreterError("Problem Planting Seed");
            }
            return new InterpreterError($"Field has different crop than provided: expected {field.CropData}, got {crop}");
        }
        return new InterpreterError($"No Field found with name {fieldName}");
    }

    public InterpreterError? PlaceItemInList(string cropName, string fieldName)
    {
        if (productDictionary.TryGetValue(ProductVariableKind.CROP, out var crops))
        {
            if (System.Array.Find(crops, (x) => x.ProductName == cropName) is CropData crop)
            {
                return PlaceItemInList(crop, fieldName);
            }
            return new InterpreterError($"No crop found with name {cropName}");

        }
        return new InterpreterError($"No crops defined");
    }

    public InterpreterError? WaterAllPlants(string fieldName)
    {
        if (Fields.TryGetValue(fieldName, out var field))
        {
            field.WaterUpdate();
            return NO_ERROR;
        }
        return new InterpreterError($"No Field found with name {fieldName}");
    }

    public InterpreterError? FeedAllAnimals(string penName)
    {
        return new InterpreterError($"UNIMPLEMENTED FUNCTION: called {nameof(FeedAllAnimals)} with value {penName}");
    }

    public InterpreterError? HarvestItemInList(CropData crop, string fieldName)
    {
        if (Fields.TryGetValue(fieldName, out var field))
        {
            if (field.CropData == crop)
            {
                return Storage.HarvestCropFromField(field) ? NO_ERROR : new InterpreterError("Problem Harvesting Crop");
            }
            return new InterpreterError($"Field has different crop than provided: expected {field.CropData}, got {crop}");
        }
        return new InterpreterError($"No Field found with name {fieldName}");
    }

    public InterpreterError? HarvestItemInList(string cropName, string fieldName)
    {
        if (productDictionary.TryGetValue(ProductVariableKind.CROP, out var crops))
        {
            if (System.Array.Find(crops, (x) => x.ProductName == cropName) is CropData crop)
            {
                return HarvestItemInList(crop, fieldName);
            }
            return new InterpreterError($"No crop found with name {cropName}");
        }
        return new InterpreterError($"No crops defined");
    }

    public InterpreterError? DiscardAllItems(string listName)
    {
        if (Fields.TryGetValue(listName, out var list))
        {
            list.DiscardAll();
            return NO_ERROR;
        }
        // else if() for pens
        return new InterpreterError($"No Field or Pen found with name {listName}");
    }

    public InterpreterError? MakeXAction(string recipeName)
    {
        if (productDictionary.TryGetValue(ProductVariableKind.RECIPE, out var recipeList))
        {
            if (System.Array.Find(recipeList, match: (x) => x.ProductName == recipeName) is RecipeData RD)
            {
                // Debug.LogWarning(Storage.StorageStringify());
                return Storage.CreateNewProduct(RD) ? NO_ERROR : new InterpreterError($"Could not create new product {RD.outputProduct.ProductName}, not enough inputs!");
            }
            return new InterpreterError($"Recipe with name {recipeName} not found!");
        }
        return new InterpreterError($"No recipe list defined!");

    }
    public InterpreterError? SellXAction(string productName)
    {
        foreach (var products in productDictionary.Values)
        {
            // foreach (var p in products)
            // {
            //     Debug.LogWarning(p.Name);
            // }
            if (System.Array.Find(products, match: (x) => x.Name == productName) is ProductData PD)
            {
                // Debug.LogWarning(Storage.StorageStringify());
                return Storage.SellProduct(PD) switch
                {
                    SellResult.SOLD_LAST => NO_ERROR,
                    SellResult.SOLD_ONE => NO_ERROR,
                    SellResult.REMOVED_EMPTY => new InterpreterError($"Could not sell {productName}, but removed from inventory!"),
                    SellResult.NOT_SOLD => new InterpreterError($"Could not sell {productName}, as there is none!"),
                    _ => new InterpreterError($"Unknown error: Could not sell {productName}!"),
                };
            }
        }
        return new InterpreterError($"No product named {productName} found in storage!");
    }
    # endregion

    #region Other Functions
    public void DebugGrowAll()
    {
        foreach (var field in Fields.Values)
        {
            field.GrowthUpdate();
        }
    }
    #endregion
}

public struct InterpreterError
{
    public string message;

    public InterpreterError(string msg)
    {
        message = msg;
    }
}

public enum ProductVariableKind
{
    CROP, ANIMAL, RECIPE, AGRICULTURAL, ALL_PRODUCTS
}