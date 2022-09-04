using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldSystem : MonoBehaviour
{
    [EditorButton(nameof(GrowthUpdate))]
    public List<CropData> Crops;
    [SerializeField] Vector2Int GridSize = new(3, 3);
    public Vector2Int GetGridSize => GridSize;
    [EditorButton(nameof(RandomizeField))]
    public FieldCrop[] subField;

    public UnityEvent OnFieldUpdate = new();
    private void Awake()
    {
        subField = new FieldCrop[GridSize.x * GridSize.y];
    }

    public FieldCrop GetFieldCrop(int location)
    {
        location = Mathf.Clamp(location, 0, subField.Length - 1);
        return subField[location];
    }
    public void GrowthUpdate()
    {
        foreach (var crop in subField)
        {
            if (crop != null && crop.CropID >= 0 && crop.CropID < Crops.Count)
            {
                var currentCrop = Crops[crop.CropID];
                if (crop.currentGrowthLevel < currentCrop.growthLevel)
                {
                    crop.currentGrowthLevel++;
                }
                else if (crop.witherTimer > 0)
                {
                    crop.witherTimer--;
                }
                else
                {
                    crop.IsWithered = true;
                }
            }
        }
        OnFieldUpdate.Invoke();
    }
    public float TotalWaterConsumption()
    {
        float runningTotal = 0;
        foreach (var crop in subField)
        {
            if (crop != null && crop.CropID < Crops.Count)
            {
                var currentCrop = Crops[crop.CropID];
                runningTotal += currentCrop.waterconsumption;
            }
        }
        return runningTotal;
    }
    public void WaterUpdate(float waterGiven)
    {
        //Q1: How do we split waterGiven between all the crops after every update?
        //Q2: How do we determine if the plant has met the overall water requirements?
        //Q3: How do we calculate if daily water requirements are met?
        OnFieldUpdate.Invoke();
    }
    public bool PlantSeed(int CropIDToPlant, int location)
    {
        //check if field is empty
        location = Mathf.Clamp(location, 0, subField.Length - 1);
        subField[location] = new()
        {
            CropID = CropIDToPlant,
            currentGrowthLevel = 0,
            witherTimer = Crops[CropIDToPlant].witherTimer,
            waterAmount = 0,
        };
        OnFieldUpdate.Invoke();
        return true;
    }
    public bool DiscardCrop(int location)
    {
        if (subField == null) return false;
        location = Mathf.Clamp(location, 0, subField.Length - 1);
        subField[location] = null;
        OnFieldUpdate.Invoke();
        return true;
    }
    public bool TryHarvestCrop(int location, out CropData crop)
    {
        crop = null;
        if (subField == null) return false;
        location = Mathf.Clamp(location, 0, subField.Length - 1);
        var currentCrop = subField[location];
        if (currentCrop != null
        && !currentCrop.IsWithered
        && currentCrop.currentGrowthLevel
        == Crops[currentCrop.CropID].growthLevel)
        {
            crop = Crops[currentCrop.CropID];
            return DiscardCrop(location);
        }
        OnFieldUpdate.Invoke();
        return false;
    }
    public List<CropData> HarvestAllCrops()
    {
        List<CropData> HarvestedCrops = new();
        for (int i = 0; i < subField.Length; i++)
        {
            if (TryHarvestCrop(i, out var harvested))
            {
                HarvestedCrops.Add(harvested);
            }
        }
        return HarvestedCrops;
    }
    public void RandomizeField()
    {
        subField = new FieldCrop[GridSize.x * GridSize.y];
        for (int i = 0; i < subField.Length; i++)
        {
            PlantSeed(Random.Range(0, Crops.Count), i);
        }
    }

    public void DiscardAll()
    {
        for (int i = 0; i < subField.Length; i++)
        {
            DiscardCrop(i);
        }
    }

    public void PlantAll(int cropID)
    {
        cropID = Mathf.Clamp(cropID, 0, Crops.Count - 1);
        subField = new FieldCrop[GridSize.x * GridSize.y];
        for (int i = 0; i < subField.Length; i++)
        {
            PlantSeed(cropID, i);
        }
    }

    // #if UNITY_EDITOR
    // For In-Editor Unity Testing ONLY
    [SerializeField, EditorButton(nameof(TestDiscard)), EditorButton(nameof(TestHarvest))] int testLocation = 0;
    private void TestDiscard()
    {
        DiscardCrop(testLocation);
    }
    private void TestHarvest()
    {
        if (TryHarvestCrop(testLocation, out var harvestedCrop))
        {
            Debug.Log($"Successfully Harvested: {harvestedCrop}");
        }

    }
    // #endif
}
[System.Serializable]
public class FieldCrop
{
    public int CropID = -1;
    public int currentGrowthLevel = -1;
    public int witherTimer = -1;
    public float waterAmount = -1;
    public WaterStatus waterStatus;
    public bool IsWithered = false;
}
public enum WaterStatus
{
    HYDRATED, OVERWATERED, DEHYDRATED
}
