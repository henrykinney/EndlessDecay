using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Tilemap TilesToCorrupt;
    public TileBase RedGrass;
    public TileBase GreenGrass;
    public float MoveSpeed;
    public float Damage;
    public float MaxHealth;
    public float Health;
    public float GoldAmount;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movedirection = GameObject.Find("Player").transform.position - gameObject.transform.position;
        float m = movedirection.magnitude;
        if (m > 0) {
            movedirection /= m;
            rb.velocity = movedirection * MoveSpeed;
        }
        else {
            rb.velocity = new Vector2(0, 0);
        }
        //Currupt();
    }
    public void DealDamage(float value) {
        Health -= value;
        if (Health <= 0) {
            Kill();
        }
    }
    public void Kill() {
        Destroy(gameObject);
        GameObject.Find("Player").GetComponent<PlayerStats>().AddGold(GoldAmount);
    }
}
