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

    public Transform fireDes;



    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(FOV());
        //StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        if(PublicVars.AlarmBot){
            StopCoroutine(FOV());
            StopCoroutine(Patrol());
        }
        StartCoroutine(FOV());
        StartCoroutine(Patrol());

    }

    IEnumerator FOV(){
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while(true){
            yield return wait;
            FOVCheck();
        }
    }

    IEnumerator LookForPlayer(){
        while(true){
            if(!canSee){
                navAgent.destination = transform.position;
                break;
            }
            yield return new WaitForSeconds(.5f);
            navAgent.destination = player.transform.position;
        }
        
    }

    IEnumerator Patrol(){
        while(true){
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

    void FOVCheck(){
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

    private void OnTriggerEnter(Collider other) {
        if(!PublicVars.AlarmBot){
            if (other.CompareTag("Player") && canSee) {
                //SceneManager.LoadScene("Level" + levelToLoad);
                if(PublicVars.lives <= 0){
                    SceneManager.LoadScene("Dead");
                }
                PublicVars.lives -= 1;
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            // isTouching = true;
        }
    }

}



