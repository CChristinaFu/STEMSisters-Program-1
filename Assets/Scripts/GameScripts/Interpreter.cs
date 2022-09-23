using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Toolbox.Editor;
using UnityEngine.Events;
using System.Linq;

public class Interpreter : BE2_TargetObject
{
    [Header("Helper Variables")]
    [SerializeField] private SerializedDictionary<ProductVariableKind, ProductData[]> productDictionary = new();


    [Header("External References")]
    [SerializeField] private GameObject fieldPrefab;
    [field: SerializeField]
    public StorageScript Storage { get; private set; }
    [field: SerializeField]
    public MoneySystem Money { get; private set; }
    [field: SerializeField]
    public Dictionary<string, FieldSystem> Fields { get; private set; }


    public class UEvent_List_Var : UnityEvent<List<string>> { }
    public UEvent_List_Var OnVariableUpdate = new();

    public ProductData[] GetProductKind(ProductVariableKind kind)
    {
        if (kind == ProductVariableKind.ALL_PRODUCTS)
        {
            // Combine Crop, Animal, and Recipe Products into a single array
            return GetProductKind(ProductVariableKind.CROP)
                .Concat(GetProductKind(ProductVariableKind.ANIMAL))
                .Concat(GetProductKind(ProductVariableKind.RECIPE))
                .ToArray();
        }
        else if (productDictionary.TryGetValue(kind, out var products))
        {
            return products;
        }
        return new ProductData[0];
    }

    public bool CreateField(string fieldName, string cropName)
    {
        // FIXME: Field should be placed by user

        if (productDictionary.TryGetValue(ProductVariableKind.CROP, out var crops)
        && System.Array.Find(crops, (x) => x.ProductName == cropName) is CropData crop)
        {
            GameObject newField = Instantiate(fieldPrefab);
            FieldSystem fieldSystem = newField.GetComponent<FieldSystem>();
            fieldSystem.SetFieldCrop(crop);
            Fields.Add(fieldName, fieldSystem);
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

    # region Actions

    public InterpreterError? PlaceItemInList(CropData crop, string fieldName)
    {
        if (Fields.TryGetValue(fieldName, out var field))
        {
            if (field.CropData == crop)
            {
                if (field.PlantFirstAvailableSeed()) return null;
                else return new InterpreterError("Problem Planting Seed");
            }
            return new InterpreterError($"Field has different crop than provided: expected {field.CropData}, got {crop}");
        }
        return new InterpreterError($"No Field found with name {fieldName}");
    }

    public InterpreterError? WaterAllPlants(string fieldName)
    {
        if (Fields.TryGetValue(fieldName, out var field))
        {
            // TODO: Water Crops Function needed
        }
        return new InterpreterError($"No Field found with name {fieldName}");
    }

    public InterpreterError? HarvestItemInList(CropData crop, string fieldName)
    {
        if (Fields.TryGetValue(fieldName, out var field))
        {
            if (field.CropData == crop)
            {
                if (Storage.HarvestCropFromField(field)) return null;
                else return new InterpreterError("Problem Harvesting Crop");
            }
            return new InterpreterError($"Field has different crop than provided: expected {field.CropData}, got {crop}");
        }
        return new InterpreterError($"No Field found with name {fieldName}");
    }
    public InterpreterError? MakeXAction(string recipeName)
    {
        if (productDictionary.TryGetValue(ProductVariableKind.RECIPE, out var recipeList))
        {
            if (System.Array.Find(recipeList, match: (x) => x.ProductName == recipeName) is RecipeData RD)
            {
                Storage.CreateNewProduct(RD);
                return null;
            }
        }
        return new InterpreterError("sum error occured");

    }
    public InterpreterError? SellXAction(string productName)
    {
        foreach (var products in productDictionary.Values)
        {
            if (System.Array.Find(products, match: (x) => x.ProductName == productName) is ProductData PD)
            {
                Storage.SellProduct(PD);
                return null;
            }
        }
        return new InterpreterError("sum error occured");
    }
    # endregion
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
    CROP, ANIMAL, RECIPE, ALL_PRODUCTS
}