using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cantPass : MonoBehaviour
{
    Collider m_Collider;
    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            m_Collider.enabled = true;
        }
        //m_Collider.enabled = false;
    }

    private void OnTriggerExit(Collider other){
        m_Collider.enabled = false;
    }
}
