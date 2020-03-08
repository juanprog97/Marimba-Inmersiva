using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController : MonoBehaviour
{
    
    private int index = 0;
    void start()
    {
        UnityEngine.XR.XRSettings.enabled = true;
    }
    public int getIndex()
    {
        return this.index;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index++;
            if (index > 2)
            {
                index = 0;
            }
            Debug.Log(index);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            index--;
            if (index < 0)
            {
                index = 2;
            }
            Debug.Log(index);
        }
   

    }
    

    // Update is called once per frame
}
