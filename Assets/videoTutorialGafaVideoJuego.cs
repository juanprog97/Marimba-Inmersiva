using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoTutorialGafaVideoJuego : MonoBehaviour
{

    public GameObject ArCamera;
    public GameObject ImageTarget;
    public GameObject Lights;
    public GameObject code_control;
    private string cancionElegida;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    public void jugar()
    {
        ArCamera.SetActive(true);
        ImageTarget.SetActive(true);
        Lights.SetActive(true);
        code_control.GetComponent<GameControl>().iniciarJuego(this.cancionElegida);
        gameObject.SetActive(false);
    }

    public void setCancion(string cancion)
    {
        this.cancionElegida = cancion;
    }

}
