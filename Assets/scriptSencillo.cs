using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class scriptSencillo : MonoBehaviour
{
    public GameObject texto;
    private GameObject bluetooth;

    void Start()
    {
 
        
    }

    void Update()
    {
        texto.GetComponent<TextMeshProUGUI>().text = componentBluetooth.Instance.dataRecived;
    }
    public void cambiarScene()
    {
        SceneManager.LoadSceneAsync("Scene2");
    }

    public void reconnect()
    {
        GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<componentBluetooth>().reconnect();
        
    }
}
