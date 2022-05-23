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
    public PlayerStats plrstats;
    public Tilemap TilesToCorrupt;
    public TileBase RedGrass;
    public TileBase GreenGrass;
    float EnemyCollidingDamage;
    public bool MovementEnabled;
    TextMeshProUGUI healthmeter;
    float RespawnTime;
    public GameObject EquippedWeapon;
    public GameObject WeaponHolder;
    public UnityEvent OnWeaponEquipped;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        charmovement = gameObject.GetComponent<CharacterMovement>();
        hp = gameObject.GetComponent<Health>();
        plrstats = gameObject.GetComponent<PlayerStats>();
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
        
        Vector3 targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetpos -= gameObject.transform.position;
        float targetangle = Vector2.SignedAngle(new Vector2(1, 0), new Vector2(targetpos.x, targetpos.y));
        
        gameObject.transform.rotation = Quaternion.Euler(0, 0, targetangle);
            
        if (EquippedWeapon != null) {
            targetangle = targetangle * Mathf.PI / 180;
            EquippedWeapon.GetComponent<Weapon>().FireAngle = targetangle;

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
    public void UpdateWeaponStats(GameObject weapon) {
        weapon.GetComponent<Weapon>().DamageModifiers.AddModifier(plrstats.WeaponDamageMult, gameObject, "PlayerStatBonus");
        weapon.GetComponent<Weapon>().FireRateModifiers.AddModifier(plrstats.WeaponFireRateMult, gameObject, "PlayerStatBonus");
        weapon.GetComponent<Weapon>().PierceModifiers.AddModifier(plrstats.WeaponPierceMult, gameObject, "PlayerStatBonus");
    }
    public GameObject GetEquippedWeapon() {
        return EquippedWeapon;
    }
    public void EquipWeapon(GameObject weapon) {
        EquippedWeapon.transform.SetParent(null);
        EquippedWeapon.transform.position += new Vector3(0, 2, 0);
        EquippedWeapon.GetComponent<Weapon>().IsFiring = false;
        EquippedWeapon.GetComponent<Weapon>().RemoveModfiersFrom(gameObject);
        EquippedWeapon = weapon;
        weapon.transform.SetParent(WeaponHolder.transform);
        weapon.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        UpdateWeaponStats(weapon);
        
        OnWeaponEquipped.Invoke();
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
    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.layer == 8) {
            EquipWeapon(trigger.gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D enemy) {
        EnemyCollidingDamage -= enemy.gameObject.GetComponent<EnemyMovement>().Damage;
    }
}
