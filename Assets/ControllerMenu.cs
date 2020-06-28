using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMenu : MonoBehaviour
{
    public GameObject MenuConfiguracion;
    public GameObject MenuPrincipal;
    public GameObject VideoGame;
    public GameObject History;
    public GameObject CameraAR;
    private int indexScene;


    
    void Start()
    {
        invokeConfigurationBluetooth();
    }

    public void invokeConfigurationBluetooth()
    {
        this.CameraAR.SetActive(false);
        this.indexScene = 0;
        UnityEngine.XR.XRSettings.enabled = false;
        this.MenuConfiguracion.SetActive(true);
        this.MenuPrincipal.SetActive(false);
        this.VideoGame.SetActive(false);
        this.History.SetActive(false);


    }
   
public void invokeMenuPrincipal()
    {
        UnityEngine.XR.XRSettings.enabled = true;
      
        this.indexScene = 1;
        this.CameraAR.SetActive(true);
        this.MenuConfiguracion.SetActive(false);
        this.MenuPrincipal.SetActive(true);
        
        this.VideoGame.SetActive(false);
        this.History.SetActive(false);


    }

    public void invokeGame()
    {
        this.indexScene = 2;
        this.MenuConfiguracion.SetActive(false);
        this.MenuPrincipal.SetActive(false);
        this.VideoGame.SetActive(true);
        this.History.SetActive(false);
    }

    public void invokeHistory()
    {
        this.indexScene = 3;
        this.MenuConfiguracion.SetActive(true);
        this.MenuPrincipal.SetActive(false);
        this.VideoGame.SetActive(false);
        this.History.SetActive(true);
    }


    public int getIndexScene()
    {
        return this.indexScene;

    }

    // Update is called once per frame

}
