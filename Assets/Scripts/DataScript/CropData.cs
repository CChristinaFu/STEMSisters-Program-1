using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "STEMSisters_Program_1/CropData", order = 0)]
public class CropData : ProductData
{
    public int growthLevel = 2;
    public int witherTimer = 1;
    public float waterconsumption = 2.0f;
    [MinMaxSlider(0, 100)]
    public Vector2 WaterRequirements;
    public List<Sprite> GrowthLevelImages;
    public Sprite witherImage;
}