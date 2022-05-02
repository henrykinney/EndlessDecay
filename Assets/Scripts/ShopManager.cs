using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopManager : MonoBehaviour
{
    public float ShopButtonSpacing;
    public GameObject StatButtonPrefab;
    public GameObject ShopPanel;
    [SerializeField]
    public List<ShopItem> ShopItems;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (ShopItem v in ShopItems) {
            GameObject newbutton = Instantiate(StatButtonPrefab);
            newbutton.transform.SetParent(ShopPanel.transform);
            newbutton.transform.localPosition = new Vector3(0, 150 - i * ShopButtonSpacing, 0);
            newbutton.transform.localScale = new Vector3(5, 1, 1);
            newbutton.transform.GetChild(0).gameObject.GetComponent<Text>().text = v.Name + ": " + v.Cost + " Gold";
            newbutton.GetComponent<Button>().onClick.AddListener(delegate{BuyUpgrade(v);});
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenShop() {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
    }
    public void BuyUpgrade(ShopItem Upgrade) { // make this work with the onclick event!!!
        if (GameObject.Find("Player").GetComponent<PlayerStats>().Gold >= Upgrade.Cost) {
            Upgrade.BoughtEvent.Invoke();
            GameObject.Find("Player").GetComponent<PlayerStats>().AddGold(-Upgrade.Cost);
        }
    }
    
}
