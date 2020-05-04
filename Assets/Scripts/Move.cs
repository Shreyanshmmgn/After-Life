using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * PLACE THIS SCRIPT IN THE TORSO PLEASE!
 * 
 * please take note that I am a beginner scripter, with only 1 month of C# experience. This code is very much spaghetti code and there are a million ways I could've done better 
 * to make the performance run better. But, I made this entire script in one day, so i am pretty happy about myself. Plus, a lot of people were asking to make a script to allow
 * the ragdoll to move around, so i hope this helps you.
 * 
 * PLEASE NOTE ABOUT THE REFERENCES
 * Assign the rigidbodies reference to their respective parts.
 * 
 * For the colliders, leftcollider = left leg, rightcollider = right leg.
 * 
 * Layermask, assign it to the environment or ground layer, make a new one if you don't have one already.
 */
public class Move : MonoBehaviour
{
    //Reference the RigidBodies
    public Rigidbody2D headRB;
    public Rigidbody2D torsoRB;
    public Rigidbody2D leftLegRB;
    public Rigidbody2D rightLegRB;

    //Reference the colliders, left collider being left leg, and right collider being right leg.
    public Collider2D leftCollider;
    public Collider2D rightCollider;

    //How fast do you want it to go?
    public float speed = 6f;

    //Without this force, the ragdoll would just lay on the ground. This force allows it to stand up and not just flop everywhere.
    public float standupForce = 0.5f;
    
    //When using addforce, the acceleration can get really out of hand, so use this float to cap the speed so it doesnt go flying 1 million miles per hour. (Hyperbole)
    public float maxSpeed = 25f;

    //You want to create 2 empty objects and put them where the foot would be. Make sure that they are the child of the leg though.
    public Transform groundCheckL;
    public Transform groundCheckR;

    //Layermask, assign this to your ground layer, or your environment.
    public LayerMask L_Layermask;
    public LayerMask R_Layermask;

    //I found that 0.2f for the ground radius is best or else it just looks funny. Anyways this is for the overlap circle later.
    public float GroundRadius = 0.2f;

    //Is it grounded?
    bool IsGrounded;

    private void Start()
    {
        //Ignores collision between the left leg and the right leg. Makes it very more smooth.
        Physics2D.IgnoreCollision(leftCollider, rightCollider);
    }
    private void Update()
    {
        //I made it so that if atleast one foot is touching the ground, that would count as grounded.
        if(Physics2D.OverlapCircle(groundCheckL.position, GroundRadius, L_Layermask))
        {
            IsGrounded = true;
        }

        if (Physics2D.OverlapCircle(groundCheckR.position, GroundRadius, R_Layermask))
        {
            IsGrounded = true;
        }
        //Some code from up there.

        
        //Only if both feet are off the ground will that count as not grounded.
        if (!Physics2D.OverlapCircle(groundCheckL.position, GroundRadius, L_Layermask))
        {
            if (!Physics2D.OverlapCircle(groundCheckR.position, GroundRadius, R_Layermask))
            {
                IsGrounded = false;
            }
        }

        //Clamp the velocity of the rigidbodies so they dont go flying.
        if(torsoRB.velocity.magnitude > maxSpeed)
        {
            torsoRB.velocity = Vector3.ClampMagnitude(torsoRB.velocity, maxSpeed);
        }
        if (leftLegRB.velocity.magnitude > maxSpeed)
        {
            leftLegRB.velocity = Vector3.ClampMagnitude(leftLegRB.velocity, maxSpeed);
        }
        if (rightLegRB.velocity.magnitude > maxSpeed)
        {
            rightLegRB.velocity = Vector3.ClampMagnitude(rightLegRB.velocity, maxSpeed);
        }
    }

    void FixedUpdate()
    {
        //All the movement is here.
        if (Input.GetKey(KeyCode.A))
        {
            //I experimented, and I noticed that you have to add forces to the torso, leg, and right leg to make it smooth.
            torsoRB.AddForce(Vector2.left * speed,ForceMode2D.Force);
            
            //The reason why this is multiplied by 2 is so that leg is more in front, looks more realistic.
            leftLegRB.AddForce(Vector2.left * speed * 2, ForceMode2D.Force);
            //The reason why this is divided by 1.25 is so that the leg is more in the back, looks more realistic.
            rightLegRB.AddForce(Vector2.left * speed / 1.25f, ForceMode2D.Force);
        }

        //Some more stuff.
        if (Input.GetKey(KeyCode.D))
        {
            torsoRB.AddForce(Vector2.right * speed, ForceMode2D.Force);
            leftLegRB.AddForce(Vector2.right * speed / 1.25f, ForceMode2D.Force);
            rightLegRB.AddForce(Vector2.right * speed * 2, ForceMode2D.Force);
        }

        //This is where the stand up force is, again, making sure that the ragdoll doesnt just flop over, :P
        if (IsGrounded == true)
        {
            headRB.AddForce(Vector2.up * standupForce, ForceMode2D.Force);
        }
    }
}