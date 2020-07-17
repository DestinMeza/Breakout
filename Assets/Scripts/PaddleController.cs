using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public enum PlayState{
        Human,
        AI,
    }
    public PlayState playState = PlayState.Human;
    Rigidbody2D rb;
    public float maxSpeedChange = 2;
    public float speed = 5;
    float targetXVelocity;
    Vector2 velocityChange;
    bool hasBall = true;
    Vector2 targetDir;
    BallController ball;
    Animator anim;
    float x;
    bool onWall = false;
    Collider2D collider;
    ContactPoint2D[] contacts = new ContactPoint2D[5];
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        ball = FindObjectOfType<BallController>();
        anim = GetComponent<Animator>();
        collider = GetComponentInChildren<Collider2D>();
    }

    void Update(){
        if(playState == PlayState.Human){
            x = Input.GetAxis("Horizontal");
            targetDir = new Vector2(x, 0);
            Move(x);
        }
        else{
            Vector3 ballPos = ball.transform.position;
            Vector3 diff = ballPos - transform.position;
            if(ball.ballState == BallController.BallState.Play) Move(diff.x);
            else if(hasBall){
                targetDir = Vector3.zero - transform.position;
                Move(targetDir.x);
                if(transform.position.magnitude < 1){
                    hasBall = false;
                    ball.LaunchBall();
                }
            }
        }
        onWall = collider.GetContacts(contacts) > 0;
        anim.SetBool("TouchingWall", onWall);
    }

    void Move(float x){
        targetXVelocity = Mathf.Clamp(x, -1, 1) * speed;
    }

    void OnEnable(){
        ball.onBallReset += ResetPaddle;
    }

    void ResetPaddle(){
        hasBall = true;
    }

    void FixedUpdate(){
        targetDir.y = 0;
        velocityChange = Vector2.right * targetXVelocity - rb.velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxSpeedChange, maxSpeedChange);
        rb.AddForce(Vector2.right * velocityChange, ForceMode2D.Impulse);
        anim.SetFloat("xVel", rb.velocity.x);
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Ball")){
            anim.SetTrigger("Hit");
        }
    }

    void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.CompareTag("Ball")){
            Vector3 v = col.rigidbody.velocity;
            Vector3 diff = col.transform.position - transform.position;
            col.rigidbody.velocity = Vector3.Lerp(v.normalized, diff.normalized, .5f) * ball.speed;
        }
    }
}
