using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    [SerializeField] private int currentTurn =0;
    [SerializeField] private TextMeshProUGUI turnText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextTurn() 
    {
        currentTurn ++;
        UpdateText();
    }
    public void UpdateText()
    {
        turnText.text = $"turn: {currentTurn} ";  
    }

}
