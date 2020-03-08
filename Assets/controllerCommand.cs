using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerCommand : MonoBehaviour
{
    private BluetoothController command;
    // Start is called before the first frame update
    void Start()
    {
        this.command = GameObject.Find("Bluetooth").GetComponent<BluetoothController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.command.getCommand());
        
    }
}
