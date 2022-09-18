using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldSystem))]
public class ShowFieldCrop : MonoBehaviour
{
    [SerializeField] FieldSystem field;
    [SerializeField] GameObject spriteVisualPrefab;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Vector2 spacing = Vector2.one;
    // Start is called before the first frame update
    void Start()
    {
        if (field == null)
        {
            field = GetComponent<FieldSystem>();
        }
        var gridSize = field.GetGridSize;
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                var subFieldSprite = Instantiate(spriteVisualPrefab, this.transform);
                subFieldSprite.transform.localPosition = new Vector3(i * spacing.x, j * spacing.y);

            }
        }
    }

    private void OnEnable()
    {
        field.OnFieldUpdate.AddListener(FieldUpdate);
    }

    private void OnDisable()
    {
        field.OnFieldUpdate.RemoveListener(FieldUpdate);
    }

    void FieldUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var crop = field.GetFieldCrop(i);
            SpriteRenderer spriteRenderer = transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (crop != null && spriteRenderer != null)
            {
                if (!crop.IsWithered)
                {
                    spriteRenderer.sprite = field.CropData.GrowthLevelImages[crop.currentGrowthLevel];
                }
                else
                {
                    spriteRenderer.sprite = field.CropData.witherImage;
                }
            }
            else
            {
                spriteRenderer.sprite = defaultSprite;
            }
        }
    }
}
