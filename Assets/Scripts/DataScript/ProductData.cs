using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductData", menuName = "STEMSisters_Program_1/ProductData", order = 0)]
public class ProductData : ScriptableObject
{
    [field: SerializeField]
    public virtual Sprite ProductImage { get; protected set; }
    [field: SerializeField]
    public virtual int ProductPrice { get; protected set; } = 1;
    [field: SerializeField]
    public virtual string ProductName { get; protected set; }

    public virtual string Name => ProductName;
}