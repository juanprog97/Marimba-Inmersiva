using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseApp : MonoBehaviour
{
 

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

 


    public void loadHistory()
    {
        SceneManager.LoadScene("MenuMultimediaInteractivo");
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
