using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class option1 : MonoBehaviour { 

    public GameObject indexView;
    public GameObject component;
void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            if (indexView.GetComponent<MenuController>().getIndex() == 0 )
            {
                
            }
            else
            {

            }
        
    }
}
