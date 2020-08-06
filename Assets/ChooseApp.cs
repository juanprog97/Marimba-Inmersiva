using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.XRSettings.enabled = false;
    }
    public void loadHistory()
    {
        UnityEngine.XR.XRSettings.enabled = true;
    }
    public void loadGame()
    {
        UnityEngine.XR.XRSettings.enabled = true;
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
