using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     public Rigidbody2D rigidbody;
     public int speed = 10;
    //  public Vector2 = new Vector2(1,0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float dir = Input.GetAxis("Horizontal");
        if(Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(Vector2.right* speed);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(Vector2.left* speed);
        }else if(Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(Vector2.up* speed);
        }
    }
}
