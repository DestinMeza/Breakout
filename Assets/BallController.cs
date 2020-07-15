using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxBallSpeed = 5;
    public float ballLaunchSpeed = 6;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        transform.up = rb.velocity;
    }

    void Start(){
        rb.AddForce(Vector2.up * ballLaunchSpeed, ForceMode2D.Impulse);
    }
}
