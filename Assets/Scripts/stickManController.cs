using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickManController : MonoBehaviour {
    public Rigidbody2D hip;
    public LimbTesting handR;
    public LimbTesting handL;
    public LimbTesting legR;
    public LimbTesting legL;

    public float hipForce = 500;
    public float addedForce = 1f;
    public float moveSpeed = 500;
    public float jumpForce = 500;
    int frameIndex = 0;
    int dir = 1;

    public int Jumps = 2;
    public int JumpCharge = 1;

    private float jtime = 0;

    void Update () {
        RotateTo (hip, 0, hipForce);

        var direction = Input.GetAxis ("Horizontal");
        if (Mathf.Abs (direction) > 0) {
            hip.AddForce (moveSpeed * direction * Vector2.right);
            if (frameIndex > 10) {
                frameIndex = 0;
                dir *= -1;
            }
            legL.SetPosition (Vector2.right * 10 * dir + Vector2.down * 7);
            legR.SetPosition (-Vector2.right * 10 * dir + Vector2.down * 7);
            frameIndex++;
        }

        if (Input.GetKeyDown (KeyCode.Space)) {
            // hip.AddForce (jumpForce * Vector2.up, ForceMode2D.Impulse);
            Jump();
            // Debug.Log ("Jumping");
        }
    }
    void RotateTo (Rigidbody2D rigidbody, float angle, float force) {
        angle = Mathf.DeltaAngle (transform.eulerAngles.z, angle);

        var x = angle > 0 ? 1 : -1;
        angle = Mathf.Abs (angle * .1f);
        if (angle > 2) {
            angle = 2;
        }
        angle *= .5f;
        angle *= (1 + angle);

        // rigidbody.angularVelocity *= .5f;
        rigidbody.AddTorque (angle * force * addedForce * x);
        // addedForce = 1f;
    }

    public void SetGrounded (bool value) {
        if (value) {
            if (jtime > Time.timeSinceLevelLoad) {
                return;
            }
            JumpCharge = Jumps;
        }
    }
    public void Jump () {
        if (JumpCharge > 0) {
            hip.velocity = Vector2.zero;
            hip.AddForce (jumpForce * Vector2.up, ForceMode2D.Impulse);
            JumpCharge--;
            jtime = Time.timeSinceLevelLoad + .1f;
        }
    }
}