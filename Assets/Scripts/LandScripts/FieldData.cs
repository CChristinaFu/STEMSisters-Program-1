using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldData : MonoBehaviour
{
    [EditorButton(nameof(GrowthUpdate))]
    public List<CropData> Crops;
    [SerializeField] Vector2Int GridSize = new(3, 3);
    [EditorButton(nameof(RandomizeField))]
    public FieldCrop[] subField;
    private void Awake()
    {
        subField = new FieldCrop[GridSize.x * GridSize.y];
    }
    public void RandomizeField()
    {
        for (int i = 0; i < subField.Length; i++)
        {
            PlantSeed(Random.Range(0, Crops.Count), i);
        }
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
        return true;
    }
    public bool DiscardCrop(int location)
    {
        location = Mathf.Clamp(location, 0, subField.Length - 1);
        subField[location] = null;
        return true;
    }
    public bool TryHarvestCrop(int location, out CropData crop)
    {
        crop = null;
        location = Mathf.Clamp(location, 0, subField.Length - 1);
        var currentCrop = subField[location];
        if (currentCrop != null
        && !currentCrop.IsWithered
        && currentCrop.currentGrowthLevel
        == Crops[currentCrop.CropID].growthLevel)
        {
            crop = Crops[currentCrop.CropID];
            DiscardCrop(location);
            return true;
        }
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
    [SerializeField, EditorButton(nameof(TestDiscard))] int testLocation = 0;
    public void TestDiscard()
    {
        DiscardCrop(testLocation);

    }
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
