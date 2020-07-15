using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeedChange = 2;
    public float speed = 5;
    Vector2 targetDir;
    BallController ball;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        ball = FindObjectOfType<BallController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        targetDir = new Vector2(x, 0);
    }

    void FixedUpdate(){
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
