using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Lifespan;
    float RemainingLifespan;
    public int Pierce;
    int RemainingPierce;
    public float Damage;

    // Start is called before the first frame update
    void Start()
    {
        RemainingLifespan = Lifespan;
        RemainingPierce = Pierce;
    }

    // Update is called once per frame
    void Update()
    {
        RemainingLifespan -= Time.deltaTime;
        if (RemainingLifespan <= 0) {
            Destroy(gameObject);
        }
    }
    void OnHitEnemy(GameObject enemy) {
        RemainingPierce -= 1;
        if (RemainingPierce <= 0) {
            Destroy(gameObject);

        }
        //Destroy(enemy);
        enemy.GetComponent<Health>().TakeDamage(Damage);
        
    }
    void OnTriggerEnter2D(Collider2D enemy) {
        if (enemy.gameObject.tag == "Enemy") {
            OnHitEnemy(enemy.gameObject);
        }
    }
}
