using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Projectile;
    public float ProjectileSpeed;
    public float FireRate;
    float FireTime;
    public int ProjectilePierce;
    // Start is called before the first frame update
    void Start()
    {
        FireTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FireTime += FireRate * Time.deltaTime;
        if (Input.GetButton("Fire1") && FireTime > 1) {
            FireTime = 0;
            GameObject newprojectile =  Instantiate(Projectile);
            newprojectile.transform.position = gameObject.transform.position;

            Vector3 targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetpos.z = 0;

            Vector3 targetvel = (targetpos - gameObject.transform.position);
            targetvel = targetvel / targetvel.magnitude;
            
            newprojectile.GetComponent<Rigidbody2D>().velocity = targetvel * ProjectileSpeed;

            newprojectile.GetComponent<Projectile>().Pierce = ProjectilePierce;
        }
    }
    public void AddFireRate(float v) {
        FireRate += v;
    }
    public void AddPierce(int v) {
        ProjectilePierce += v;
    }
}
