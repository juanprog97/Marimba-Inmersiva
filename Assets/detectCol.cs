using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectCol : MonoBehaviour
{
   
    public int key;
    Animation a_pulse;
    public GameObject Code;
    [System.Obsolete]
    void Start()
    {
        GameControl detect = Code.GetComponent<GameControl>();
        detect.PUSH += Detect_PUSH;
        this.a_pulse = this.gameObject.transform.FindChild("Pulsador").GetComponent<Animation>();
    }

    private void Detect_PUSH(object sender, System.EventArgs e)
    {
        a_pulse.Play("shootPusle");
    }


    /* void Update()
     {
         //Event e = Event.current;
         Event e = Event.current;
         if (Input.GetKeyDown(KeyCode.X))
         {
             a_pulse.Play("shootPusle");
         }
     }*/

   /* public void action()
    {
        //Event e = Event.current;
        a_pulse.Play("shootPusle");
       /* if (command[key] == '1' || e.isKey)
        {
           
        }

    }*/
    

}
