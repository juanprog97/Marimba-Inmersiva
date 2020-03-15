using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController : MonoBehaviour
{
    
    private int index = 0;
    private String command_t;
    private bool enter;
    public GameObject parent;
    public GameObject status_T;


    void start()
    {
        UnityEngine.XR.XRSettings.enabled = true;
        this.enter = false;
        
    }
    public int getIndex()
    {
        return this.index;
    }

    public bool getEnterValue()
    {
        return this.enter;
       
    }


    void Update()
    {
        this.command_t = parent.GetComponent<vuforiaScript>().comma(); 
        
        if (this.command_t[1] == '1')
        {
            this.index++;
            if (this.index > 2)
            {
                this.index = 0;
            }
         
        }
        if(this.command_t[0] == '1')
        {
            this.index--;
            if (this.index < 0)
            {
                this.index = 2;
            }
            
        }
        status_T.GetComponent<UnityEngine.UI.Text>().text = this.command_t;
        /* if (this.command_t[2] == '1')
         {
             this.enter = true;
         }
         else
         {
             this.enter = false;
         }*/


    }
    

    // Update is called once per frame
}
