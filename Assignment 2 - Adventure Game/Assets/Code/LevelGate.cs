using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGate : MonoBehaviour
{
    public int levelToLoad;
    TransitionManager _transitionManager;
    void Start() 
    {
        _transitionManager = FindObjectOfType<TransitionManager>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && PublicVars.canAdvance) {
            PublicVars.canAdvance = false;
            _transitionManager.LoadScene("Level" + levelToLoad);
        }
    }
}
