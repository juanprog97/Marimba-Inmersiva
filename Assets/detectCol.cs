using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectCol : MonoBehaviour
{
   
    public KeyCode key;
    Animation a_pulse;

    [System.Obsolete]
    void Start()
    {
       
        this.a_pulse = this.gameObject.transform.FindChild("Pulsador").GetComponent<Animation>();
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (Input.GetKeyDown(this.key))
        {
            a_pulse.Play("shootPusle");
        }
    }
    

}
