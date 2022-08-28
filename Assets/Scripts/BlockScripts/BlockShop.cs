using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockShop : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject labelPrefab;
    [SerializeField] MoneySystem marketplace;
    [SerializeField] BlockCategory[] Categories;
    // Start is called before the first frame update
    void Start()
    {
        foreach (BlockCategory category in Categories)
        {
            var label = Instantiate(labelPrefab, this.transform);
            if (label.GetComponent<TextMeshProUGUI>() is TextMeshProUGUI text)
            {
                text.text = category.categoryName;
            }
            foreach (BlockInformation blockinfo in category.blocks)
            {
                var block = Instantiate(blockPrefab, this.transform);
                Debug.Log(blockinfo.blockName);
            }
        }
    }

    // Update is called once per frame
    public void BuyBlock(GameObject blockToBuy, int blockPrice)
    {
        if (marketplace.HasEnoughMoney(blockPrice))
        {
            marketplace.UpdateMoney(-blockPrice);
            var cloneBlock = Instantiate(blockToBuy);
        }
    }
}
[System.Serializable]
public class BlockCategory
{
    public string categoryName;
    public BlockInformation[] blocks;
}
