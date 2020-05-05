using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour {

    public GameManager manager;
    [SerializeField]
    public GameObject player;


    private void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Lava") {
            player.SetActive (false);
            manager.OnOfAudio(false);
            manager.OnOffPanel(true);

        }
    }

    private void Update() {
        
    }
}