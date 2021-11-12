using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    TransitionManager _transitionManager;

    // Start is called before the first frame update
    void Start(){
        _transitionManager = FindObjectOfType<TransitionManager>();
        Resume();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PublicVars.paused) { Resume(); }
            else
            {
                pauseMenu.SetActive(true);
                PublicVars.paused = true;
                //Time.timeScale = 0;
            }
        }
    }

    public void Resume(){
        //Time.timeScale = 1;
        pauseMenu.SetActive(false);
        PublicVars.paused = false;
        //
    }

    public void Home(){
        _transitionManager.LoadScene("Beginning");
    }

    public void Quit(){
        Application.Quit();
    }

    public void Play(){
        PublicVars.lives = 3;
        _transitionManager.LoadScene("Video");
    }
}
