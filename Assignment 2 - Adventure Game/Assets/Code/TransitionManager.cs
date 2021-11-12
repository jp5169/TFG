using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    float maxVol = .3f;
    public AudioClip[] clips;
    private float fadeSpeed = .2f;
    private AudioSource[] audioSources;
    private int trackIndex = 0;
    public Image fadeImg;

    void Awake() 
    {
        if(FindObjectsOfType<TransitionManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            audioSources = GetComponents<AudioSource>();
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        StartCoroutine(FadeMusic(audioSources[trackIndex], audioSources[trackIndex = 1 - trackIndex],scene.buildIndex));
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeMusic(AudioSource fadeIn, AudioSource fadeOut, int buildIndex)
    {
        print(buildIndex + "build");
        fadeIn.clip = clips[buildIndex];
        fadeIn.Play();

        float t = 0;
        while(t < 1)
        {
            fadeIn.volume = Mathf.SmoothStep(0, maxVol, t);
            fadeOut.volume = Mathf.SmoothStep(maxVol, 0, t);
            t += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        fadeOut.Stop();
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(.2f);
        fadeImg.CrossFadeAlpha(0,1,true);
        yield return new WaitForSeconds(1);
        fadeImg.gameObject.SetActive(false);

    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }
    IEnumerator FadeAndLoad(string sceneName)
    {
        fadeImg.gameObject.SetActive(true);
        fadeImg.CrossFadeAlpha(1,1,true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
    void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
