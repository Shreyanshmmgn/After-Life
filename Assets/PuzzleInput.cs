using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleInput : MonoBehaviour {
    string ans, word;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject eye2, player;
    [SerializeField]
    private Text outPut;
    void Start () {

    }
    int length = 0;
    bool ansCorrect = false;
    void Update () {
        Behaviour foPlayer = (Behaviour) player.GetComponent ("stickManController");
        // player.GetComponent<FollowPlayer>().false;
        foPlayer.enabled = false;
        if (Input.GetKey (KeyCode.Backspace)) {
            ans = ans.Remove (ans.Length - 1);
        }
        if (!ansCorrect) {
            length++;
            word = Input.inputString;
            WordChecker (word);
        }
        if (ans == "ice" || ans == "ICE") {
            // Chest Opens!!
            ansCorrect = true;
            // player.SetActive (true);
            foPlayer.enabled = true;
            animator.SetInteger ("Chest", 1);
            eye2.SetActive (true);
            outPut.text = "";
        }

        void WordChecker (string word) {
            ans = ans + word;
            outPut.text = "Enter :- " + ans;
            Debug.Log ("Ans --- " + ans + "  " + "Word -- " + word);
        }
    }
}