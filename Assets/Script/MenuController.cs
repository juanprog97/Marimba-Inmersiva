using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private int index = 0;
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
