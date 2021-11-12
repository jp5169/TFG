using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireAlarm : MonoBehaviour
{
    public GameObject textwarning;
    public GameObject cantUse;
    public bool alarmUsed = false;

    public AudioClip alarm;
    AudioSource _as;

    //public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PublicVars.AlarmBot){
            _as.Stop();
        }
    }


    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Player") && PublicVars.hammer) {
            _as.clip = alarm;
            _as.Play();
            textwarning.SetActive(false);
            PublicVars.AlarmBot = true;
            alarmUsed = true;
            PublicVars.hammer = false;
        }
        else if(other.CompareTag("Player") && PublicVars.hammer == false && !alarmUsed){
            textwarning.SetActive(true);
        }
        else if(other.CompareTag("Player") && PublicVars.hammer == false && alarmUsed)
        {
            textwarning.SetActive(false);
            cantUse.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            textwarning.SetActive(false);
            cantUse.SetActive(false);
        }
    }
}
