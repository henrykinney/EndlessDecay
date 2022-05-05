using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    CharacterMovement charmovement;
    Health hp;
    public Tilemap TilesToCorrupt;
    public TileBase RedGrass;
    public TileBase GreenGrass;
    public float Damage;
    public float GoldAmount;
    public float XpAmount;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        charmovement = gameObject.GetComponent<CharacterMovement>();
        hp = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movedirection = GameObject.Find("Player").transform.position - gameObject.transform.position;
        float moveangle = Vector2.SignedAngle(new Vector2(1, 0), movedirection);
        moveangle = moveangle * Mathf.PI / 180;
        charmovement.MoveAngle = moveangle;
        //Currupt();
    }
    public void Kill() {
        Destroy(gameObject);
        GameObject.Find("Player").GetComponent<PlayerStats>().AddGold(GoldAmount);
        GameObject.Find("Player").GetComponent<PlayerStats>().AddXp(XpAmount);
    }
}
