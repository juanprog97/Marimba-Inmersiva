using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class scriptSencilo2 : MonoBehaviour
{
    public GameObject texto;
    public string comand;
    void Start()
    {
        
    }

    void Update()
    {

        texto.GetComponent<TextMeshProUGUI>().text =  componentBluetooth.Instance.dataRecived;
    }
    // Update is called once per frame
    public void cambiarScene()
    {
        SceneManager.LoadSceneAsync("Scene1");
    }
}
