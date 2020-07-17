using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTypeController : MonoBehaviour
{
    public List<GameObject> brickTypes;
    public int brickTypeID = 0;
    public GameObject brick;
    void Start(){
        CreateBrick();
    }
    void CreateBrick(){
        if(brickTypeID < brickTypes.Count && brickTypeID >= 0){
            GameObject brickType = brickTypes[brickTypeID];
            brick = Instantiate(brickType);
            brick.transform.parent = transform;
            brick.transform.localPosition = Vector3.zero;
        }
    }

    public void SetBrick(int indexer){
        brickTypeID = indexer;
        while(brickTypeID >= brickTypes.Count){
            brickTypeID -= brickTypes.Count;
        }
        SpriteRenderer nextBrick = brickTypes[brickTypeID].GetComponent<SpriteRenderer>();
        brick.GetComponent<SpriteRenderer>().sprite = nextBrick.sprite;
        gameObject.SetActive(true);
    }

    void OnDrawGizmos(){
        Gizmos.DrawCube(transform.position, Vector3.one);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, -transform.forward);
    }
}