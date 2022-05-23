using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesManager : MonoBehaviour
{
    public GameObject UpgradesPanel;
    public int PointsRemaining;
    public TextMeshProUGUI PointsText;
    public GameObject UpgradeButton;
    public TextMeshProUGUI WeaponInfoText;
    
    // Start is called before the first frame update
    void Start()
    {
        RefreshPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddUpgradePoints(int v) {
        PointsRemaining += v;
        PointsText.text = "Points Remaining: " + PointsRemaining;
    }
    public void UpgradeCurrentWeapon() {
        if (PointsRemaining <= 0) {
            return;
        }
        bool wasupgraded = UpgradeWeapon(GameObject.Find("Player").GetComponent<PlayerMovement>().EquippedWeapon);
        if (wasupgraded) {
            AddUpgradePoints(-1);
        }
    }
    public bool UpgradeWeapon(GameObject weapon) {
        int currentlevel = weapon.GetComponent<WeaponUpgrades>().CurrentLevel;
        
        if (currentlevel >= weapon.GetComponent<WeaponUpgrades>().Upgrades.Count) {
            return false;
        }

        WeaponStatInfo newupgrade = weapon.GetComponent<WeaponUpgrades>().Upgrades[currentlevel];
        weapon.GetComponent<Weapon>().FireRate += newupgrade.FireRate;
        weapon.GetComponent<Weapon>().ProjectilePierce += newupgrade.Pierce;
        weapon.GetComponent<Weapon>().ProjectileDamage += newupgrade.Damage;
        weapon.GetComponent<Weapon>().ProjectileSpeed += newupgrade.Speed;
        weapon.GetComponent<WeaponUpgrades>().CurrentLevel += 1;
        RefreshPanel();
        return true;
    }
    public void ToggleUpgrades() {
        UpgradesPanel.SetActive(!UpgradesPanel.activeSelf);
    }
    public void CloseUpgrades() {
        UpgradesPanel.SetActive(false);
    }
    string GetUpgradeText(GameObject weapon) {
        string result = "";
        int currentlevel = weapon.GetComponent<WeaponUpgrades>().CurrentLevel;

        if (currentlevel >= weapon.GetComponent<WeaponUpgrades>().Upgrades.Count) {
            return "";
        }

        WeaponStatInfo upgradestats = weapon.GetComponent<WeaponUpgrades>().Upgrades[currentlevel];
        if (upgradestats.FireRate != 0) {
            result += "Fire Rate: " + upgradestats.FireRate + "\n";
        }
        if (upgradestats.Pierce != 0) {
            result += "Pierce: " + upgradestats.Pierce + "\n";
        }
        if (upgradestats.Damage != 0) {
            result += "Damage: " + upgradestats.Damage + "\n";
        }
        if (upgradestats.Speed != 0) {
            result += "Speed: " + upgradestats.Speed + "\n";
        }
        return result;
    }
    public void RefreshPanel() {
        GameObject currentweapon = GameObject.Find("Player").GetComponent<PlayerMovement>().GetEquippedWeapon();
        WeaponInfoText.text = GetUpgradeText(currentweapon);
    }
}
