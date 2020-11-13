using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controllerGafas : MonoBehaviour
{

    private int opcion;
    public GameObject tutorialJuego;

    void OnEnable()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    public void comenzar()
    {
        if (this.opcion == 1)
        {
            SceneManager.LoadScene("History");
          
        }
        else
        {
            SceneManager.LoadScene("Cultura");
        }
    }
    public void setOpcionModulo(int opcion)
    {
        this.opcion = opcion;
    }
    public void regresar()
    {
        tutorialJuego.SetActive(true);
        gameObject.SetActive(false);
    }
}
