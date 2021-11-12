using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FiredMenu : MonoBehaviour
{
    TransitionManager _transitionManager;
    // Start is called before the first frame update
    void Start()
    {
        _transitionManager = FindObjectOfType<TransitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Home(){
        _transitionManager.LoadScene("Beginning");
    }

    public void Quit(){
        Application.Quit();
    }

    public void Restart(){
        _transitionManager.LoadScene("Level1");
    }
}
