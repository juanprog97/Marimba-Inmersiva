using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseApp : MonoBehaviour
{
    public GameObject textoComando;

    [Obsolete]
    void Awake()
    {
        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                UnityEngine.XR.XRSettings.enabled = false;
               
            }
            else
            {
                Debug.Log("Error");
            }
        });

    }

    [Obsolete]
    void OnEnable()
    {
        componentBluetooth.Instance.seTocoBoton += esperandoComando;
    }
    [Obsolete]
    void OnDisable()
    {
        componentBluetooth.Instance.seTocoBoton -= esperandoComando;
    }


[System.Obsolete]

 
    private void esperandoComando(object sender, EventArgs e)
    {
        textoComando.GetComponent<Text>().text = componentBluetooth.Instance.dataRecived;
    }

    public void loadHistory()
    {
        UnityEngine.XR.XRSettings.enabled = true;
    }
    public void loadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
