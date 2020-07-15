using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTools.Components;

public class GameManager : MonoBehaviour
{
    public List<BrickTypeController> bricks;
    public static GameManager gameManager;
    BallController mainBall;
    public int score = 0;
    public int brickValue = 5;
    public int bricksActive = 0;
    public int level = 1;
    void Awake(){
        bricksActive = bricks.Count;
        mainBall = FindObjectOfType<BallController>();
    }
    void OnEnable(){
        HealthController.onAnyDeath += IncreaseScore;
    }
    void IncreaseScore(HealthController h){
        score += 5;
        bricksActive--;
        if(bricksActive <= 0){
            Debug.Log("You Won!");
            Reset();
        }
    }
    void Reset(){
        mainBall.ResetBall();
        bricksActive = bricks.Count;
        level++;
        for(int i = 0; i < bricks.Count; i++){
            bricks[i].SetBrick(level);
        }
    }
}
