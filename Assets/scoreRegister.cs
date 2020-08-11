using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scoreRegister : MonoBehaviour
{
    public GameObject puntaje;
    public GameObject nUsuario;
    public void rellenar(int nPuntaje)
    {
        puntaje.GetComponent<Text>().text = nPuntaje.ToString();
    }

    public void registrar()
    {
       
        Debug.Log(nUsuario.GetComponent<InputField>().text);
        nUsuario.GetComponent<InputField>().text = "";
    }


    // Update is called once per frame
    
}
