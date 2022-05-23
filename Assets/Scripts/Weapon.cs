using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Projectile;
    public float ProjectileSpeed;
    public float ProjectileDamage;
    public float FireRate;
    float FireTime;
    public int ProjectilePierce;
    public bool IsFiring;
    public float FireAngle;
    public ModifierBank SpeedModifiers;
    public ModifierBank DamageModifiers;
    public ModifierBank FireRateModifiers;
    public ModifierBank PierceModifiers;
    
    // Start is called before the first frame update
    void Start()
    {
        SpeedModifiers = new ModifierBank();
        DamageModifiers = new ModifierBank();
        FireRateModifiers = new ModifierBank();
        PierceModifiers = new ModifierBank();
        FireTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FireTime += FireRate * FireRateModifiers.GetMult() * Time.fixedDeltaTime;
        if (!IsFiring && FireTime > 1) {
            FireTime = 1;
        }
        if (IsFiring && FireTime >= 1) {
            FireTime -= 1;
            FireProjectile(Projectile);
        }
        
    }
    public void FireProjectile(GameObject projectileprefab) {
        GameObject newprojectile =  Instantiate(projectileprefab);
        newprojectile.transform.position = gameObject.transform.position;

        Vector3 targetvel = new Vector3(Mathf.Cos(FireAngle), Mathf.Sin(FireAngle), 0f);
        
        newprojectile.GetComponent<Rigidbody2D>().velocity = targetvel * ProjectileSpeed * SpeedModifiers.GetMult();

        newprojectile.GetComponent<Projectile>().SetPierce(ProjectilePierce * Mathf.FloorToInt(PierceModifiers.GetMult()));
        newprojectile.GetComponent<Projectile>().Damage = ProjectileDamage * DamageModifiers.GetMult();
    }
    public void AddFireRate(float v) {
        FireRate += v;
    }
    public void AddPierce(int v) {
        ProjectilePierce += v;
    }
    public void AddDamage(float v) {
        ProjectileDamage += v;
    }
    public void RemoveModfiersFrom(GameObject user) {
        FireRateModifiers.RemoveModifier(user, null);
    }
}
