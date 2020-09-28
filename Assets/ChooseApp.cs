using Firebase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseApp : MonoBehaviour
{
    // Start is called before the first frame update
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
    void Start()
    {
       
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
