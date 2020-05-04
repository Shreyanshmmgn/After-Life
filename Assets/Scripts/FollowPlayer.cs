using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public Transform player;
    public Transform minX, minY;
    public Transform maxX, maxY;
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        Vector3 positionPlayer;
        positionPlayer = player.transform.position;
        
        positionPlayer.x =  Mathf.Clamp (positionPlayer.x, minX.position.x, maxX.position.x);
        positionPlayer.y = Mathf.Clamp (positionPlayer.y, minY.position.y, maxY.position.y);
        transform.position = positionPlayer;

    }
}