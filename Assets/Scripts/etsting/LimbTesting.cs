using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbTesting : MonoBehaviour {

    private List<LimbPartTesting> parts = new List<LimbPartTesting> ();
    public Transform root;
    public Vector2 resetPosition;
    public float force = 50;
    [Range (0f, 1f)]
    public float ratio = 0.9f;

    public bool flipped = false;
    public bool sin = false;
    [Space]
    public bool resting = false;
    void Start () {
        var hinge = GetComponent<HingeJoint2D> ();
        while (hinge != null && hinge.transform != root) {
            parts.Add (new LimbPartTesting (hinge.attachedRigidbody, flipped, sin));
            hinge = hinge.connectedBody.GetComponent<HingeJoint2D> ();
        }

        parts.Reverse ();
    }

    public void SetPosition (Vector2 targetPos) {
        currentPos = targetPos + (Vector2) root.position;
        resetPos = false;
    }

    bool resetPos = true;
    Vector2 currentPos;
    void FixedUpdate () {
        var force = this.force;
        if (resetPos) {
            currentPos = resetPosition + (Vector2) root.position;
            if (resting) {
                force *= 0.1f;
            }
        } else {
            resetPos = true;

        }
        var index = parts.Count;
        foreach (var part in parts) {
            part.RotateToward (currentPos, force * (index * (index + 1)) * 0.5f, ratio);
            index--;

        }
        // parts[0].RotateToward (targetPosition + (Vector2) root.position, force, ratio);

    }

    private void OnDrawGizmos () {
        foreach (var p in parts) {
            p.OnDrawGizmos ();
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere (resetPosition + (Vector2) root.position, .1f);
    }
}