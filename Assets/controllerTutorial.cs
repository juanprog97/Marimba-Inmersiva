using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerTutorial : MonoBehaviour
{
    public GameObject gafasTutorial;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }


    public void saltaTutorial()
    {
        gafasTutorial.SetActive(true);
        gameObject.SetActive(false);
    }

    
}
