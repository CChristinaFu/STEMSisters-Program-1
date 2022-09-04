using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AnimalData", menuName = "STEMSisters_Program_1/AnimalData", order = 0)]
public class AnimalData : ProductData
{
    public string animalName;
    public float foodConsumption = 2;
    public int animalStage = 2;
    public List<Sprite> animalStageImage;

}