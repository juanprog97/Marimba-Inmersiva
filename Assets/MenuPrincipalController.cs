using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalController : MonoBehaviour
{
    public GameObject JuegoMenu;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
    void Start()
    {
        
    }

    public void entrarJuegoMenu()
    {
        this.JuegoMenu.SetActive(true);
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
