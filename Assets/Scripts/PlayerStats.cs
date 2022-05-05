using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    public float Gold;
    public float Xp;
    public float MaxXp;
    public int Level;
    public UnityEvent OnLevelUp;
    public UnityEvent OnXpChanged;
    public UnityEvent OnGoldChanged;

    TextMeshProUGUI goldtext;
    TextMeshProUGUI xptext;
    // Start is called before the first frame update
    void Start()
    {
        goldtext = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
        xptext = GameObject.Find("XpText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelUp() {
        Xp -= MaxXp;
        MaxXp = MaxXp * 1.2f + 4f;
        Level++;
        OnLevelUp.Invoke();
    }
    public void AddXp(float value) {
        Xp += value;
        if (Xp >= MaxXp) {
            LevelUp();
        }
        OnXpChanged.Invoke();
        
    }
    public void AddGold(float value) {
        Gold += value;
        OnGoldChanged.Invoke();
        
    }
}
