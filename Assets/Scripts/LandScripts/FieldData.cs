using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldData : MonoBehaviour
{
public List<CropData> Crops;
public FieldCrop[][] subField;
public void GrowthUpdate(){
    foreach (var sf in subField)
    {
        foreach (var crop in sf)
        {
        if (crop.CropID<Crops.Count){
            //check if over growthlevel, show plant withers
            crop.currentGrowthLevel ++;
        }
        }        
    }
}
}
public class FieldCrop
{
    public int CropID;
    public int currentGrowthLevel;
    public float waterAmount;
}