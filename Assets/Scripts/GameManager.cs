using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    [SerializeField]
    private AudioSource audioi;
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject hiddenPlatform;
    Animator animator;

    // public GameObject[] checkPoint;
    Transform currentCheckPoint;

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
}