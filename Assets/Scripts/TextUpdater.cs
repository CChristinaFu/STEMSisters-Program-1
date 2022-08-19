using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text textItem;
    [SerializeField] string prefixString = "";
    [SerializeField] string suffixString = "";
    void Awake()
    {
        if (textItem == null)
        {
            textItem = GetComponent<TMP_Text>();
        }
    }

    public void UpdateTextWithString(string newValue)
    {
        textItem.text = $"{prefixString}{newValue}{suffixString}";
    }

    public void UpdateTextWithFloat(float newValue)
    {
        textItem.text = $"{prefixString}{newValue}{suffixString}";
    }

    public void UpdateTextWithInt(int newValue)
    {
        textItem.text = $"{prefixString}{newValue}{suffixString}";
    }

}
