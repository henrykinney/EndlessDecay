using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    CharacterMovement charmovement;
    Health hp;
    public Tilemap TilesToCorrupt;
    public TileBase RedGrass;
    public TileBase GreenGrass;
    float EnemyCollidingDamage;
    public bool MovementEnabled;
    TextMeshProUGUI healthmeter;
    float RespawnTime;
    public GameObject EquippedWeapon;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        charmovement = gameObject.GetComponent<CharacterMovement>();
        hp = gameObject.GetComponent<Health>();
        EnemyCollidingDamage = 0;
        healthmeter = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        RespawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MovementEnabled) {
            Vector2 movedirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (movedirection.magnitude == 0) {
                charmovement.IsMoving = false;
            }
            else {
                charmovement.IsMoving = true;
                float moveangle = Vector2.SignedAngle(new Vector2(1, 0), movedirection);
                moveangle = moveangle * Mathf.PI / 180;
                charmovement.MoveAngle = moveangle;
            }
            Currupt();
        }
        if (EquippedWeapon != null) {
            if (MovementEnabled) {
                EquippedWeapon.GetComponent<Weapon>().IsFiring = Input.GetButton("Fire1");
            }
            else {
                EquippedWeapon.GetComponent<Weapon>().IsFiring = false;
            }
        }

    }
    void FixedUpdate() {
        hp.TakeDamage(EnemyCollidingDamage * Time.fixedDeltaTime);
        if (RespawnTime >= 0) {
            RespawnTime -= Time.fixedDeltaTime;
            healthmeter.text = "Respawn Time: " + Mathf.FloorToInt(RespawnTime);
            if (RespawnTime <= 0) {
                RespawnPlayer();
            }
        }
    }
    public void Kill() {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        MovementEnabled = false;
        RespawnTime = 10;
    }
    void RespawnPlayer()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        MovementEnabled = true;
        hp.Revive();
        gameObject.transform.position = new Vector3(-45, -45, 0);
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
