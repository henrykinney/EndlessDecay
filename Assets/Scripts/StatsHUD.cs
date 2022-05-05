using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsHUD : MonoBehaviour
{
    public TextMeshProUGUI goldtext;
    public TextMeshProUGUI xptext;
    public TextMeshProUGUI healthtext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateGold(PlayerStats stats) {
        goldtext.text = "Gold: " + Mathf.FloorToInt(stats.Gold);
    }
    public void UpdateXp(PlayerStats stats) {
        xptext.text = "Level: " + stats.Level + " Xp: " + Mathf.FloorToInt(stats.Xp) + "/" + Mathf.CeilToInt(stats.MaxXp);
    }
    public void UpdateHealth(Health hp) {
        healthtext.text = "Health: " + Mathf.CeilToInt(hp.CurrentHealth) + "/" + Mathf.CeilToInt(hp.MaxHealth);
    }
}
