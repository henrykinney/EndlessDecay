using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public Tilemap TilesToCorrupt;
    public TileBase RedGrass;
    public TileBase GreenGrass;
    public float MoveSpeed;
    public float Health;
    public float MaxHealth;
    float EnemyCollidingDamage;
    public bool MovementEnabled;
    TextMeshProUGUI healthmeter;
    float RespawnTime;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        EnemyCollidingDamage = 0;
        healthmeter = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        RespawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MovementEnabled) {
            Vector2 movedirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            float m = movedirection.magnitude;
            if (m > 0) {
                movedirection /= m;
                rb.velocity = movedirection * MoveSpeed;
            }
            else {
                rb.velocity = new Vector2(0, 0);
            }
            Currupt();
        }
        Health -= EnemyCollidingDamage * Time.deltaTime;
        healthmeter.text = "Health: " + Mathf.CeilToInt(Health) + "/" + Mathf.CeilToInt(MaxHealth);
        if (Health <= 0 && RespawnTime <= 0) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            MovementEnabled = false;
            RespawnTime = 10;
        }
        if (RespawnTime >= 0) {
            RespawnTime -= Time.deltaTime;
            healthmeter.text = "Respawn Time: " + Mathf.FloorToInt(RespawnTime);
            if (RespawnTime <= 0) {
                RespawnPlayer();
            }
        }
    }
    void RespawnPlayer()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        MovementEnabled = true;
        Health = MaxHealth;
        gameObject.transform.position = new Vector3(-45, -45, 0);
    }
    public void AddMaxHealth(float value) {
        Health += value;
        MaxHealth += value;
    }
    void Currupt()
    {
        
        Vector3Int tilepos = TilesToCorrupt.WorldToCell(gameObject.transform.position);
        if (TilesToCorrupt.GetTile(tilepos) == GreenGrass) {
            TilesToCorrupt.SetTile(tilepos, RedGrass);
        }
    }
    void OnCollisionEnter2D(Collision2D enemy) {
        
        if (enemy.gameObject.tag == "Enemy") {
            EnemyCollidingDamage += enemy.gameObject.GetComponent<EnemyMovement>().Damage;
        }
    }
    void OnCollisionExit2D(Collision2D enemy) {
        EnemyCollidingDamage -= enemy.gameObject.GetComponent<EnemyMovement>().Damage;
    }
}
