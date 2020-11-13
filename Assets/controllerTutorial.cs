using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerTutorial : MonoBehaviour
{
    public GameObject gafasTutorial;
    public GameObject MenuPrincipal;
    void OnEnable()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }


    public void saltaTutorial()
    {
        gafasTutorial.SetActive(true);
        gameObject.SetActive(false);
    }

    public void regresar()
    {
        MenuPrincipal.SetActive(true);
        gameObject.SetActive(false);
    }

    
}
