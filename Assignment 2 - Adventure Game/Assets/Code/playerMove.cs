using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerMove : MonoBehaviour
{
    public GameObject smallRadius;
    public GameObject bigRadius;

    public Animator anim;
    NavMeshAgent navAgent;
    Camera mainCam;
    private const float DOUBLE_CLICK_TIME = .2f;
    private float lastClickTime = -1;
    //float noise = 0;
    float startTime;
    float endTime;
    //RaycastHit hit;
    Vector3 hitP;
    Vector3 initialPos;
    bool gotFirst = false;
    bool running = false;
    bool walking = false;
    //bool moving = false;
    //bool firstMove = false;
    //float noise = 0;
    float increment = 1f;
    float ran;
    Vector3 pos;
    public AudioClip walk;
    public AudioClip run;
    AudioSource _audiosrc;
    void Start()
    {
        _audiosrc= GetComponent<AudioSource>();
        navAgent = GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
        // StartCoroutine(NoiseDecrement());
    }


    void Update()
    {
        //check if idle
        if (pos == transform.position) {
                bigRadius.SetActive(false);
                smallRadius.SetActive(true);
                PublicVars.enemyRadius = 3;
        }

        pos = transform.position;


        if(Input.GetMouseButtonDown(0))
        {
            //firstMove = true;
            //moving = true;
            initialPos = transform.position;
            //print("initialPos: " + initialPos);
            startTime = Time.time;
            //print("startTime  "+ Time.time);
            float timeSinceLastClick = Time.time - lastClickTime;
            //print("time since last clicked"+ timeSinceLastClick);
            if(timeSinceLastClick <= DOUBLE_CLICK_TIME)
            {
                //print("run");
                bigRadius.SetActive(true);
                smallRadius.SetActive(false);
                PublicVars.enemyRadius = 5;
                navAgent.speed = 8f;
                running = true;
                walking = false;
                _audiosrc.clip = run;
                _audiosrc.Play();
            }
            else{
                //print("walk");

                bigRadius.SetActive(false); 
                smallRadius.SetActive(true);
                PublicVars.enemyRadius = 3;
                navAgent.speed = 3.5f;
                running = false;
                walking = true;
                _audiosrc.clip = walk;
                _audiosrc.Play();
            }
            RaycastHit hit;
            if(Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, 200)){
                navAgent.destination = hit.point;
                hitP = hit.point;
            }
            lastClickTime = Time.time;
        }
        _audiosrc.Stop();
        anim.SetBool("Walking", false);
        
        if(hitP.x == transform.position.x && hitP.z == transform.position.z && gotFirst == false)
        {
            anim.SetBool("Walking", true);
            endTime = Time.time;
            gotFirst = true;
            //print("endTime  "+ endTime);
        }
        if(running && hitP.x != transform.position.x && hitP.z != transform.position.z)
        { 
            anim.SetBool("Walking", true);
            // PublicVars.noise += Mathf.Round(increment) * 2.75f;
            // print("current Noise: running"+ PublicVars.noise + running);
        }
        else if(walking && hitP.x != transform.position.x && hitP.z != transform.position.z)
        {
            anim.SetBool("Walking", true);
            // PublicVars.noise += Mathf.Round(increment);
            // print("current Noise: walking"+ PublicVars.noise + walking);
        }
        //print("retNoise:"+ retNoise + running);


    }
    //if does not move for too long have someone chase him?
    // IEnumerator NoiseDecrement(){
    //     while(true){
    //         if(PublicVars.noise>=0)//moving == false && firstMove
    //         {
    //             //take shorter to decrement
    //             yield return new WaitForSeconds(.1f);
    //             PublicVars.noise -= 1.5f;
    //             print("not moving, current noise: " + PublicVars.noise);
    //             //yield return new WaitForSeconds(1f);
    //         }
    //         else{
    //             yield return null;
    //         }
            
            

    //     }
    // }
    
    
}
