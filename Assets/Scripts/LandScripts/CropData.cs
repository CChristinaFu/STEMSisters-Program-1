using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "STEMSisters_Program_1/CropData", order = 0)]
public class CropData : ScriptableObject {
public string cropName;
public int cropPrice = 1;
public int growthLevel = 2;
public float waterconsumption = 2.0f;
[MinMaxSlider(0,100)] 
public Vector2 WaterRequirements;
public List<Sprite> GrowthLevelImages;

}