using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductData", menuName = "STEMSisters_Program_1/ProductData", order = 0)]
public class ProductData : ScriptableObject
{
    public Sprite productImage;
    public int productPrice = 1;
    public string productName;

}