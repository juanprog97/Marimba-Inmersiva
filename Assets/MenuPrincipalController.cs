using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalController : MonoBehaviour
{
    public GameObject tutorial;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        UnityEngine.XR.XRSettings.enabled = false;
    }


    public void entrarJuegoMenu()
    {
        this.tutorial.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void entrarLaboratorio()
    {
        Debug.Log("Entrar Laboratorio");
    }

    public void regresarMenuPrincipal()
    {
        SceneManager.LoadScene("PrincipalMenu");

    }
    
   
}
