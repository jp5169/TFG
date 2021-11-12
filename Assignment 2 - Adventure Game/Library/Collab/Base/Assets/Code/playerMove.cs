using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerMove : MonoBehaviour
{
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
    float retNoise = 0;
    float increment = 1f;
    float ran;
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
        StartCoroutine(NoiseDecrement());
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //firstMove = true;
            //moving = true;
            initialPos = transform.position;
            //print("initialPos: " + initialPos);
            startTime = Time.time;
            print("startTime  "+ Time.time);
            float timeSinceLastClick = Time.time - lastClickTime;
            if(timeSinceLastClick <= DOUBLE_CLICK_TIME)
            {
                navAgent.speed = 8;
                running = true;
                walking = false;
            }
            else{
                navAgent.speed = 3.5f;
                running = false;
                walking = true;
            }
            RaycastHit hit;
            if(Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, 200)){
                navAgent.destination = hit.point;
                hitP = hit.point;
            }
        }
        anim.SetBool("Walking", false);
        
        if(hitP.x == transform.position.x && hitP.z == transform.position.z && gotFirst == false)
        {
            anim.SetBool("Walking", true);
            endTime = Time.time;
            gotFirst = true;
            //moving = false;
            print("endTime  "+ endTime);
        }
        if(running && hitP.x != transform.position.x && hitP.z != transform.position.z)
        { 
            anim.SetBool("Walking", true);
            retNoise += Mathf.Round(increment) * 2.75f;
            print("current Noise: running"+ retNoise + running);
        }
        else if(walking && hitP.x != transform.position.x && hitP.z != transform.position.z)
        {
            anim.SetBool("Walking", true);
            retNoise += Mathf.Round(increment);
            print("current Noise: walking"+ retNoise + walking);
        }
        //print("retNoise:"+ retNoise + running);


    }
    //if does not move for too long have someone chase him?
    IEnumerator NoiseDecrement(){
        while(true){
            if(retNoise>=0)//moving == false && firstMove
            {
                //take shorter to decrement
                yield return new WaitForSeconds(1f);
                retNoise -= 1.5f;
                print("not moving, current noise: " + retNoise);
                //yield return new WaitForSeconds(1f);
            }
            else{
                yield return null;
            }
            
            

        }
    }
    
    
}
