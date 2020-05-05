using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    [SerializeField]
    private AudioSource audioi;
    [SerializeField]
    private GameObject panel;
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void SceneChanger () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1, LoadSceneMode.Single);
    }
    public void Quitt()
    {
        Application.Quit();
    }
    public void OnOfAudio (bool onoff) {
        if (onoff) { audioi.Play (); } else {
            audioi.Stop ();

        }
    }

    public void OnOffPanel (bool onoff) {
        panel.SetActive (onoff);
    }
}