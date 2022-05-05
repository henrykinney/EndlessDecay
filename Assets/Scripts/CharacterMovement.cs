using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float MoveSpeed;
    public bool MovementEnabled;
    public float MoveAngle;
    public bool IsMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() {
        Move();
    }
    void Move() {
        if (MovementEnabled && IsMoving) {
            Vector2 movedirection = new Vector2(Mathf.Cos(MoveAngle), Mathf.Sin(MoveAngle));
            rb.velocity = movedirection * MoveSpeed;
        }
        else {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
