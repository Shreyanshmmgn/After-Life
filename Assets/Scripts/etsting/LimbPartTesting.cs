using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbPartTesting {

    private Rigidbody2D rigidbody;
    private Transform transform;
    public Vector2 reltativeOffset => offset.x * transform.right + offset.y * transform.up;
    public Vector2 offset;

    Vector2 avgDirection;

    Vector2 avgWorldPos;

    private bool flip = false;
    private bool sin = false;

    public LimbPartTesting (Rigidbody2D rigidbody, bool flipped, bool sin) {
        this.rigidbody = rigidbody;

        this.transform = rigidbody.transform;

        this.flip = flipped;

        var hinge = transform.GetComponent<HingeJoint2D> ();

        offset = hinge.anchor;

        this.sin = sin;
    }
    const float wratio = .95f;
    public void RotateToward (Vector2 worldPos, float force, float ratio) {
        var diff = (worldPos - avgWorldPos).magnitude;
        if (diff > 1) {
            diff = 1;
        }

        diff = 1 - diff;

        avgWorldPos = worldPos * (1 - wratio) + avgWorldPos * wratio;
        // Debug.Log("Limb position -  " + (Vector2)transform.position);
        var direction = worldPos - (reltativeOffset + (Vector2) transform.position);
        if (flip)
            direction *= -1;
        ratio += (1 - ratio) * diff * .99f;
        avgDirection = direction * (1 - ratio) + avgDirection * ratio;
        var angle = AngleFromDirection (avgDirection);

        var cangle = transform.eulerAngles.z;
        if(sin)
            cangle -=90;

        angle = Mathf.DeltaAngle (cangle, angle);

        var x = angle > 0 ? 1 : -1;
        // Limitinf value of angle - the lower the better
        angle = Mathf.Abs (angle * 0.1f);

        if (angle > 2) {
            angle = 2;
        }
        angle *= 0.5f; // scale it back to one
        angle *= 1 + angle; // scale it back to two
        rigidbody.angularVelocity *= angle * 0.5f;
        rigidbody.AddTorque (angle * force * x);
        // Debug.Log (angle);
    }

    private float AngleFromDirection (Vector2 dir) {
        dir = dir.normalized;

        var angle = Mathf.Acos (dir.x) * Mathf.Rad2Deg;
        return dir.y > 0 ? angle : 360 - angle;
    }

    public void OnDrawGizmos () {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere ((Vector2) transform.position + reltativeOffset, .1f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere ((Vector2) transform.position - reltativeOffset, .1f);
    }
}