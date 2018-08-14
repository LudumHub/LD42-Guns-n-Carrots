using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTheGame : MonoBehaviour {
    public void StartGame()
    {
        Analytics.StartGame();
        SceneManager.LoadScene(1);
    }

    public void PlaySound ()
    {
        GetComponent<AudioSource>().Play();
    }
}
