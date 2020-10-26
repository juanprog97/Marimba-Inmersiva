using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoTutorial : MonoBehaviour
{
    public GameObject catalogoCanciones;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    // Update is called once per frame
    public void iniciarCatalogo()
    {
        this.catalogoCanciones.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
