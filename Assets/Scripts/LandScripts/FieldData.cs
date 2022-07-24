using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldData : MonoBehaviour
{
[EditorButton(nameof(GrowthUpdate))]
public List<CropData> Crops;
[SerializeField] Vector2Int GridSize = new(3,3);
[EditorButton(nameof(RandomizeField))]
public FieldCrop[] subField;
private void Awake() {
    subField= new FieldCrop[GridSize.x*GridSize.y];
}
public void RandomizeField(){
    for (int i = 0; i < subField.Length; i++){
        subField[i] = new();
        subField[i].CropID = Random.Range(0,Crops.Count);
    }
}
public void GrowthUpdate(){
    foreach (var crop in subField)
    {
    if (crop.CropID<Crops.Count){
        var currentCrop = Crops[crop.CropID];
        if (crop.currentGrowthLevel<currentCrop.growthLevel){
            crop.currentGrowthLevel ++; 
        }   
        else if (crop.witherTimer>0){
            crop.witherTimer --;
        }
        else{
            crop.IsWithered= true;
        }
    }
}
}
public float TotalWaterConsumption(){
    float runningTotal = 0;
    foreach (var crop in subField){
    if (crop != null && crop.CropID<Crops.Count){
        var currentCrop = Crops[crop.CropID];
        runningTotal+=currentCrop.waterconsumption;
    }
    }
    return runningTotal;
}
public void WaterUpdate(float waterGiven){
    //Q1: How do we split waterGiven between all the crops after every update?
    //Q2: How do we determine if the plant has met the overall water requirements?
    //Q3: How do we calculate if daily water requirements are met?
}
}
[System.Serializable]
public class FieldCrop
{
    public int CropID;
    public int currentGrowthLevel;
    public int witherTimer;
    public float waterAmount;
    public WaterStatus waterStatus;
    public bool IsWithered = false;
}
public enum WaterStatus
{
    HYDRATED, OVERWATERED, DEHYDRATED 
}
