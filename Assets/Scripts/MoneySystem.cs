using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] int currentMoney = 1_000;
    public UEvent_int OnMoneyUpdate = new();
    void Start()
    {
        OnMoneyUpdate.Invoke(currentMoney);
    }

    public void UpdateMoney(int amount)
    {
        //a negative amount should never be more than currentMoney
        currentMoney += amount;
        OnMoneyUpdate.Invoke(currentMoney);

    }
    public bool HasEnoughMoney(int moneyCheck) => moneyCheck <= currentMoney;

}