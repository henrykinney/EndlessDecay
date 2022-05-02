using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShopItem
{
    [SerializeField]
    public float Cost;
    [SerializeField]
    public string Name;
    public UnityEvent BoughtEvent;
}
