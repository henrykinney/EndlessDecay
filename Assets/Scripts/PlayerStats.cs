using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    public float Gold;
    TextMeshProUGUI goldtext;
    // Start is called before the first frame update
    void Start()
    {
        goldtext = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddGold(float value) {
        Gold += value;
        goldtext.text = "Gold: " + Mathf.FloorToInt(Gold);
    }
}
