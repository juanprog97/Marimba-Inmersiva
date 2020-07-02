using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note_s : MonoBehaviour
{
    // Start is called before the first frame update
    
    void OnTriggerEnter(Collider other)
    {

        gameObject.SendMessageUpwards("colision");
        

    }


}
