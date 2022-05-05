using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShopItem
{
    public float BaseCost;
    public float Cost;
    public float CostMult;
    public string Name;
    public int AmountBought;
    public UnityEvent BoughtEvent;
    public string GetButtonText() {
        return Name + ": " + Mathf.CeilToInt(Cost) + " Gold";
    }
    public void Buy() {
        AmountBought += 1;
        Cost = BaseCost * Mathf.Pow(CostMult, AmountBought);
    }
    public void Initialize() {
        Cost = BaseCost;
    }
}
