using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController : MonoBehaviour
{
    
    private int index = 0;
    private bool enter;
    public GameObject parent;
 
    void start()
    {
       // UnityEngine.XR.XRSettings.enabled = true;
        this.enter = false;
        
       
    }

    public void moveMenu(string commandt)
    {
        if (commandt[1] == '1')
        {
            this.index++;
            if (this.index > 2)
            {
                this.index = 0;
            }

        }
        if (commandt[0] == '1')
        {
            this.index--;
            if (this.index < 0)
            {
                this.index = 2;
            }

        }
    }

    public int getIndex()
    {
        return this.index;
    }

    public bool getEnterValue()
    {
        return this.enter;
       
    }



    
    

    // Update is called once per frame
}
