using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerGafas : MonoBehaviour
{

    private int opcion;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    public void comenzar()
    {
        if (this.opcion == 1)
        {
            Debug.Log("Historia");
        }
        else
        {
            Debug.Log("Cultura");
        }
    }
    public void setOpcionModulo(int opcion)
    {
        this.opcion = opcion;
    }
}
