using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class fovMove : MonoBehaviour
{
    NavMeshAgent navAgent;
    public int levelToLoad;
    public float radius; 
    public float angle;
    public GameObject player;
    public LayerMask targetMask;
    public LayerMask otherMask;
    public bool canSee;

    // bool isTouching = false;
    public GameObject fovCone;

    public Transform[] points;
    private int pointIndex;
    bool destination = false;
    public TransitionManager _transitionManager;

    // Start is called before the first frame update
    public void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        _transitionManager = FindObjectOfType<TransitionManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOV());
        StartCoroutine(Patrol());
    }

    public void Update()
    {
        if(PublicVars.AlarmBot){
            GetComponent<alarmMove>().enabled = true;
            GetComponent<fovMove>().enabled = false;
        }
    }

    public IEnumerator FOV(){
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while(true){
            yield return wait;
            FOVCheck();
        }
    }

    public IEnumerator LookForPlayer(){
        while(true){
            if(!canSee){
                navAgent.destination = transform.position;
                break;
            }
            yield return new WaitForSeconds(.5f);
            navAgent.destination = player.transform.position;
        }
    }

    public IEnumerator Patrol(){
        while(!PublicVars.AlarmBot){
            yield return new WaitForSeconds(.2f);
            if(!destination){
                navAgent.destination = points[pointIndex].position;
                if(transform.position.x == points[pointIndex].position.x && transform.position.z == points[pointIndex].position.z){
                    destination = true;
                    pointIndex++;
                }
            }
            else{
                if(pointIndex >= points.Length){
                    pointIndex = 0;
                }
                destination = false;
            }
            if(canSee){
                break;
            }
        }
    }

    public void FOVCheck(){
        if(!PublicVars.AlarmBot){
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
            if(rangeChecks.Length != 0){
                Transform target = rangeChecks[0].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                if(Vector3.Angle(transform.forward, dirToTarget) < angle / 2){
                    float disToTarget = Vector3.Distance(transform.position, target.position);

                    if(!Physics.Raycast(transform.position, dirToTarget, disToTarget, otherMask)){
                        canSee = true;
                        StopCoroutine(Patrol());
                        StartCoroutine(LookForPlayer());
                        
                    }
                    else{
                        canSee = false;
                        StopCoroutine(LookForPlayer());
                        StartCoroutine(Patrol());
                    }
                }
                else{
                    canSee = false;
                    StopCoroutine(LookForPlayer());
                    StartCoroutine(Patrol());
                }
            }
            else if(canSee){
                canSee = false;
                StopCoroutine(LookForPlayer());
                StartCoroutine(Patrol());
            }
        }
        StopCoroutine(LookForPlayer());
        StopCoroutine(Patrol());
        StopCoroutine(FOV());

    }

    private void OnTriggerEnter(Collider other) {
        if(!PublicVars.AlarmBot){
            if (other.CompareTag("Player") && canSee) {
                if(PublicVars.lives <= 1 || gameObject.tag == "boss"){
                    _transitionManager.LoadScene("Fired");
                    //PublicVars.lives = 3;
                    PublicVars.canAdvance = false;
                }
                else {
                    PublicVars.lives -= 1;
                    PublicVars.canAdvance = false;
                    //Destroy(gameObject);
                    _transitionManager.LoadScene(SceneManager.GetActiveScene().name);
                }

            }
        }
    }

}



