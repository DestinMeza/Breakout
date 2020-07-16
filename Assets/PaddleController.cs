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
    bool hasBall = true;
    Vector2 targetDir;
    BallController ball;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        ball = FindObjectOfType<BallController>();
    }

    void Update()
    {
        if(playState == PlayState.Human){
            float x = Input.GetAxis("Horizontal");
            targetDir = new Vector2(x, 0);
        }
        else{
            Vector3 ballPos = ball.transform.position;
            Vector3 diff = ballPos - transform.position;
            if(ball.ballState == BallController.BallState.Play) targetDir = diff;
            else if(hasBall){
                targetDir = Vector3.zero - transform.position;
                if(transform.position.magnitude < 1){
                    hasBall = false;
                    ball.LaunchBall();
                }
            }
        }
    }

    void OnEnable(){
        ball.onBallReset += ResetPaddle;
    }

    void ResetPaddle(){
        hasBall = true;
    }

    void FixedUpdate(){
        targetDir.y = 0;
        targetDir = Vector2.ClampMagnitude(targetDir, maxSpeedChange) * speed;
        rb.velocity = targetDir;
    }

    void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.CompareTag("Ball")){
            Vector3 v = col.rigidbody.velocity;
            Vector3 diff = col.transform.position - transform.position;
            col.rigidbody.velocity = Vector3.Lerp(v.normalized, diff.normalized, .8f) * ball.maxBallSpeed;
        }
    }
}
