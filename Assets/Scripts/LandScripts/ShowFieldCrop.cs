using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(FieldSystem))]
public class ShowFieldCrop : MonoBehaviour
{
    [SerializeField] FieldSystem field;
    [SerializeField] GameObject spriteVisualPrefab;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Vector2 spacing = Vector2.one;
    private Vector2 CurrentGridSize = Vector2.zero;
    // Start is called before the first frame update
    void ReinitializeGrid()
    {
        if (field == null)
        {
            field = GetComponentInParent<FieldSystem>();
        }
        if (CurrentGridSize == field.GetGridSize)
        {
            return;
        }
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        CurrentGridSize = field.GetGridSize;
        spacing = field.CropData.CropSpacing;
        for (int i = 0; i < CurrentGridSize.x; i++)
        {
            for (int j = 0; j < CurrentGridSize.y; j++)
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
        ReinitializeGrid();
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
