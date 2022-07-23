using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "AnimalData", menuName = "STEMSisters_Program_1/AnimalData", order = 0)]
public class AnimalData : ScriptableObject {
public string animalName;
public string animalProductName;
public int animalProductValue =1;
public float foodConsumption = 2.0f;
public int animalStage = 2;
public List<Sprite> animalStageImages;
    
}