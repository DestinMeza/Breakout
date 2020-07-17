using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public delegate void OnOutOfBounds();
    public OnOutOfBounds onOutOfBounds = delegate {};
    public delegate void OnBallReset();
    public OnBallReset onBallReset = delegate {};
    public enum BallState{Set, Play}
    public Vector2 setOffset = new Vector2 (0, 1);
    public BallState ballState = BallState.Set;
    Rigidbody2D rb;
    public float maxBallSpeed = 12;
    public float speed = 0;
    public float ballLaunchSpeed = 7;
    Animator anim;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update(){
        if(ballState == BallState.Set && Input.GetButton("Submit")){
            LaunchBall();
        }
        else if(ballState == BallState.Set) SetPos();
        transform.up = rb.velocity;
    }

    void Start(){
        ResetBall();
    }

    public void ResetBall(){
        PaddleController paddle = FindObjectOfType<PaddleController>();
        transform.parent = paddle.transform;
        rb.velocity = Vector2.zero;
        transform.localPosition = Vector2.zero + setOffset;
        ballState = BallState.Set;
        onBallReset();
    }

    void SetPos(){
        transform.localPosition = Vector2.zero + setOffset;
    }

    public void LaunchBall(){
        ballState = BallState.Play;
        speed = ballLaunchSpeed;
        rb.AddForce(Vector2.up * ballLaunchSpeed, ForceMode2D.Impulse);
        transform.parent = null;
    }
    
    void FixedUpdate(){
        if(rb.velocity.y < 1 && ballState == BallState.Play) rb.AddForce(transform.right); 
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxBallSpeed) * speed;
        anim.SetFloat("velocity", rb.velocity.magnitude);
    }

    void OnCollisionEnter2D(){
        if(maxBallSpeed >= speed )speed++;
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("Bounds")){
            speed = 0;
            onOutOfBounds();
            ResetBall();
        }
    }
}
