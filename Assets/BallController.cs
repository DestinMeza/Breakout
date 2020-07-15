using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public delegate void OnOutOfBounds();
    public OnOutOfBounds onOutOfBounds = delegate {};
    public enum BallState{Set, Play}
    public Vector2 setOffset = new Vector2 (0, 1);
    BallState ballState = BallState.Set;
    Rigidbody2D rb;
    public float maxBallSpeed = 5;
    public float ballLaunchSpeed = 6;
    public float maxShiftSpeed = 1;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(ballState == BallState.Set && Input.GetButton("Submit")){
            LaunchBall();
        }
        else if(ballState == BallState.Set) SetPos();
    }

    void Start(){
        ResetBall();
    }

    void ResetBall(){
        PaddleController paddle = FindObjectOfType<PaddleController>();
        transform.parent = paddle.transform;
        rb.velocity = Vector2.zero;
        ballState = BallState.Set;
    }

    void SetPos(){
        transform.localPosition = Vector2.zero + setOffset;
    }

    void LaunchBall(){
        ballState = BallState.Play;
        rb.AddForce(Vector2.up * ballLaunchSpeed, ForceMode2D.Impulse);
    }
    
    void FixedUpdate(){
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxShiftSpeed) * maxBallSpeed;
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("Bounds")){
            onOutOfBounds();
            ResetBall();
        }
    }
}
