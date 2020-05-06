using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    [SerializeField]
    private AudioSource audioi;
    [SerializeField]
    private GameObject panel, puzzleText, restart;

    [SerializeField]
    private GameObject player;

    // [SerializeField]
    // private GameObject hiddenPlatform;
    Animator animator, animatorPuzzle;

    // public GameObject[] checkPoint;
    Transform currentCheckPoint;

    private void Start() {
        Invoke("RestartButton" , 100);
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
        // Debug.Log ("Current Checkpoint -- " + currentCheckPoint);
        // player.transform.position = currentCheckPoint.position;
        // Debug.Log ("Player Position -- " + player.transform.position);
        // player.SetActive (true);
        // player.transform.position = new Vector3 (-30, 0, 0);
        // panel.SetActive (false);
        // OnOfAudio (true);
        SceneManager.LoadScene ("Game Starting");
    }

    public void ActivatePuzzle () {
        animatorPuzzle.SetInteger ("Story", 1);
    }

    public void RestartButton () {
        restart.SetActive(true);
    }
}