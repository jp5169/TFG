using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float totalLives = 3;
    public Image[] pencil;
    public Sprite emptyPencil;
    public Sprite fullPencil;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < pencil.Length; i++)
        {
            if(i < PublicVars.lives)
            {
                pencil[i].sprite = fullPencil;
            }
            else
            {
                pencil[i].sprite = emptyPencil;
            }
            if(i < totalLives)
            {
                pencil[i].enabled = true;
            }
            else
            {
                pencil[i].enabled = false;
            }
        }
    }
}
