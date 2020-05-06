using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour {
    [SerializeField]
    private GameManager manager;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject eye1, eye2;
    string tagg;
    int count = 0;
    // float jumpForce;
    private void Start () {
        // jumpForce = transform.GetComponent<stickManController> ().jumpForce;
        // rb = GetComponent<Rigidbody2D> ();
    }

    private void OnCollisionEnter2D (Collision2D other) {
        tagg = other.gameObject.tag;
        if (tagg == "UpperSpikes" || tagg == "Lava" || tagg == "LowerSpikes") {
            PlayerDied ();
            // Debug.Log ("Spikes");
        } else if (tagg == "Eye") {
            Debug.Log ("Eye Got  - " + count);
            if (count == 0) {
                eye1.SetActive (true);
                count++;
            } else {
                eye2.SetActive (true);
            }
            Destroy (other.gameObject);
        } else if (tagg == "HiddenPlatform") {

            manager.hiddenPlatformAction (other.gameObject);
        }
    }
    void PlayerDied () {
        player.SetActive (false);
        manager.OnOfAudio (false);
        manager.OnOffPanel (true);
    }
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag ("Checkpoint")) {
            manager.UpdateCheckPoint (other.gameObject.transform);
            // Destroy (other.gameObject);
            other.gameObject.SetActive (false);
        } else if (other.CompareTag ("Puzzle")) {
            manager.ActivatePuzzle ();
        }
    }

    private void Update () {
        if (transform.position.y < -10) {
            PlayerDied ();
        }
    }
}