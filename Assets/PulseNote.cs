using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseNote : MonoBehaviour
{
    public KeyCode key;
    private GameObject Pulsador;
    private bool detect = false;
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        this.Pulsador.GetComponentsInChildren<GameObject>();
    }

  
}
