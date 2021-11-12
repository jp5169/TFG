using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class bossAlarm : MonoBehaviour
{
    TransitionManager _transitionManager;
    NavMeshAgent navAgent; 

    public float timer = 10; 

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _transitionManager = FindObjectOfType<TransitionManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        GetComponent<bossMove>().enabled = false;
        navAgent.speed = 3;
        StartCoroutine(LookForPlayer());
    }

    public IEnumerator LookForPlayer(){
        while(true){
            /*if(!canSee){
                navAgent.destination = transform.position;
                break;
            }*/
            yield return new WaitForSeconds(.5f);
            navAgent.destination = player.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PublicVars.AlarmBot){
            if(timer > 0){
                timer -= Time.deltaTime;
            }
            else{
                PublicVars.AlarmBot = false;
                GetComponent<bossMove>().enabled = true;
                StartCoroutine(GetComponent<bossMove>().FOV());
                StartCoroutine(GetComponent<bossMove>().Patrol());
                GetComponent<bossAlarm>().enabled = false;

            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PublicVars.canAdvance = false;
            _transitionManager.LoadScene("Fired");
            //PublicVars.lives = 3;

        }

    }
}
