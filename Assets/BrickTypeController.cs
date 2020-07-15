using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTypeController : MonoBehaviour
{
    public List<GameObject> brickTypes;
    public int brickTypeID = 0;
    void Start(){
        CreateBrick();
    }
    void CreateBrick(){
        if(brickTypeID < brickTypes.Count && brickTypeID >= 0){
            GameObject brickType = brickTypes[brickTypeID];
            GameObject brick = Instantiate(brickType);
            brick.transform.parent = transform;
            brick.transform.localPosition = Vector3.zero;
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawCube(transform.position, Vector3.one);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, -transform.forward);
    }
}