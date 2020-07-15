using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTools.Components;

public class GameManager : MonoBehaviour
{
    public List<GameObject> bricks;
    public static GameManager gameManager;
    void Awake(){
        GameObject g = GameObject.Find("Bricks");
        Transform[] transforms = g.GetComponentsInChildren<Transform>();
        foreach(Transform t in transforms){
            bricks.Add(t.gameObject);
        }
    }
    void OnEnable(){
        
    }
}
