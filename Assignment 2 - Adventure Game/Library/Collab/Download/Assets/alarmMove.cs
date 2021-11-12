using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class alarmMove : MonoBehaviour
{
    NavMeshAgent navAgent;

    public Transform point;

    public bool destination = false;

    public float timer = 10;

    //public bool timerRunning = false;

    
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        GetComponent<fovMove>().enabled = false;
        //navAgent.updatePosition = false;
        StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        if(PublicVars.AlarmBot){
            //navAgent.isStopped = true;
            //navAgent.ResetPath();
            //navAgent.destination = point.position;
            if(timer > 0){
                timer -= Time.deltaTime;
            }
            else{
                PublicVars.AlarmBot = false;
                GetComponent<fovMove>().enabled = true;
                //GetComponent<fovMove>().enabled = true;
                //might not need 2 lines beneath
                StartCoroutine(GetComponent<fovMove>().FOV());
                StartCoroutine(GetComponent<fovMove>().Patrol());
                GetComponent<alarmMove>().enabled = false;
            }
        }
    }

    public IEnumerator Patrol(){
        yield return new WaitForSeconds(.2f);
        if(!destination){
            navAgent.destination = point.position;
            if(transform.position.x == point.position.x && transform.position.z == point.position.z){
                destination = true;
            }
        }
    }
    
}
