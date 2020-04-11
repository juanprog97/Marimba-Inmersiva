using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectCol : MonoBehaviour
{
    private bool shootNote;
    public KeyCode key;
    Animation a_pulse;
    void Start()
    {
        this.shootNote = false;
        this.a_pulse = GameObject.FindObjectOfType<Animation>();
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (Input.GetKeyDown(this.key))
        {
            a_pulse.Play("shootPusle");
        }
    }
    void Update()
    {
      /*  if (Input.GetKeyDown(this.key))
        {
            StartCoroutine(pressed());
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        this.shootNote = true;
        
        
       
    }
    void OnTriggerExit(Collider other)
    {
        this.shootNote = false;

    }
   /* IEnumerator pressed()
    {
       
        yield return new WaitForSeconds(0.4);
    }*/
}
