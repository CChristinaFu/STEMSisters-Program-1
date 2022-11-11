using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldDynamicTooltip : MonoBehaviour
{
    [SerializeField] FieldSystem field;
    [SerializeField] SimpleTooltip tooltip;
    public void FieldTooltipUpdate()
    {
        tooltip.infoLeft = $@"{field.CropData.ProductName} field = ''{field.fieldName}'';
    # of crops in field: {field.subField.Count(x => x != null)}
    Water Requirement: {field.CropData.WaterRequirements.x} - {field.CropData.WaterRequirements.y}";

    }
}
