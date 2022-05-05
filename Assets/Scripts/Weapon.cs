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
    
    // Start is called before the first frame update
    void Start()
    {
        FireTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FireTime += FireRate * Time.fixedDeltaTime;
        if (IsFiring && FireTime >= 1) {
            FireTime -= 1;
            FireProjectile(Projectile);
        }
        if (!IsFiring && FireTime > 1) {
            FireTime = 1;
        }
    }
    public void FireProjectile(GameObject projectileprefab) {
        GameObject newprojectile =  Instantiate(projectileprefab);
        newprojectile.transform.position = gameObject.transform.position;

        Vector3 targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetpos.z = 0;

        Vector3 targetvel = (targetpos - gameObject.transform.position);
        targetvel = targetvel / targetvel.magnitude;
        
        newprojectile.GetComponent<Rigidbody2D>().velocity = targetvel * ProjectileSpeed;

        newprojectile.GetComponent<Projectile>().Pierce = ProjectilePierce;
        newprojectile.GetComponent<Projectile>().Damage = ProjectileDamage;
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
}
