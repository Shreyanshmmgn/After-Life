using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    [SerializeField]
    private AudioSource audioi;
    [SerializeField]
    private GameObject panel, restart, puzzleScript;

    [SerializeField]
    private GameObject player;

    // [SerializeField]
    // private GameObject hiddenPlatform;

    Animator animator;
    [SerializeField]
    Animator animatorPuzzleCanvas;

    // public GameObject[] checkPoint;
    Transform currentCheckPoint;

    private void Start () {
        Invoke ("RestartButton", 100);
        if (SceneManager.GetActiveScene ().name == "Main") {
            animatorPuzzleCanvas.SetInteger ("Story", 2);

        }
    }

    public void UpdateCheckPoint (Transform checkPoint) {
        this.currentCheckPoint = checkPoint;
    }

    public void GoToLastCheckPoint () {
        player.transform.position = currentCheckPoint.position;
        player.SetActive (true);
    }

    public void SceneChanger () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1, LoadSceneMode.Single);
    }
    public void Quitt () {
        Application.Quit ();
    }
    public void OnOfAudio (bool onoff) {
        if (onoff) { audioi.Play (); } else {
            // audioi.Stop ();
            audioi.Pause ();
        }
    }
    public void OnOffPanel (bool onoff) {
        panel.SetActive (onoff);
    }

    public void hiddenPlatformAction (GameObject obj) {
        animator = obj.GetComponent<Animator> ();
        animator.SetBool ("Platform", true);
    }

    public void Restart () {
        // Debug.Log ("Player Position -- Starting" + player.transform.position);
        Debug.Log ("Current Checkpoint -- " + currentCheckPoint.position.x);
        // player.transform.position = currentCheckPoint.position;
        Debug.Log ("Player Position -- " + player.transform.position);
        // player.SetActive (true);
        // player.transform.position = new Vector3 (-30, 0, 0);
        // panel.SetActive (false);
        // OnOfAudio (true);
        if (currentCheckPoint.position.x > 58) {
            SceneManager.LoadScene ("Checkpoint2");
        } else if (currentCheckPoint.position.x > 31 && currentCheckPoint.position.x < 57) {
            SceneManager.LoadScene ("Checkpoint1");
        } else {
            SceneManager.LoadScene ("Checkpoint0");
        }
    }

    public void ActivatePuzzle () {
        animatorPuzzleCanvas.SetInteger ("Story", 1);
        puzzleScript.SetActive (true);

    }

    public void RestartButton () {
        restart.SetActive (true);
    }
    public void GoToMenu () {
        SceneManager.LoadScene ("Menu");
    }
    // public void GameWon () {
    //     SceneManager.LoadScene("End");
    // }
}