using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class code_ui_history : MonoBehaviour
{
  
    public GameObject boton_izquierdo;
    public GameObject boton_derecho;
    private componentBluetooth escuchaComando;
    public GameObject ContentTitulos;
    public GameObject itemTitulos;
    public Text TextoTiempo;
    public Slider slideTiempo;
    int estadoPeriodo = 0;

    private List<string> titulosPruebas = new ArrayList<string> { "Prueba1", "jajajajajajaja", "SeñorMordo", "Alop","Periodo Historico","Muchedumbre" };
    private List<string> titulosTiempo = new ArrayList<string> { "2000", "2010", "2011", "2012", "2013", "2015" };
    void Start()
    {
        UnityEngine.XR.XRSettings.enabled = true;
        componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
        GameObject item;
        this.slideTiempo.minValue = 0;
        this.slideTiempo.maxValue = titulosPruebas.Count-1;
        this.slideTiempo.value = 0;
        for (int i = 0; i< this.titulosPruebas.Count; i++)
        {
            item = Instantiate(itemTitulos);
            item.GetComponent<Text>().text = titulosPruebas[i];
            item.transform.parent = ContentTitulos.transform;
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.LeanSetPosZ(0);
        }

        
    }

    private bool soloUnComando()
    {
        int NumNotasExactas = 0;
        int notaExactaTocada = -1;
        for(int i = 0; i<12; i++)
        {
            if(componentBluetooth.Instance.dataRecived[i] == '1')
            {
                NumNotasExactas++;
                notaExactaTocada = i;
            } 
        }
        return NumNotasExactas == 1 && notaExactaTocada!= -1;
        
    }

   /* void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode.ToString() == "LeftArrow" && LeanTween.tweensRunning == 0)
            {
                
                

            }

            else if (e.keyCode.ToString() == "RightArrow" && LeanTween.tweensRunning == 0)
            {
                if(this.estadoPeriodo < titulosPruebas.Count-1)
                {
                    this.estadoPeriodo++;
                    LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x - 890, 0.25f).setEaseOutCubic();
                    TextoTiempo.GetComponent<Text>().text = titulosTiempo[this.estadoPeriodo];
                    this.slideTiempo.value = this.estadoPeriodo;
                }
                
            }
        }
    }*/


    private void botonIzquierdo()
    {
        if (LeanTween.tweensRunning == 0)
        {
            if (this.estadoPeriodo != 0)
            {
                this.estadoPeriodo--;
                LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x + 890, 0.25f).setEaseOutCubic(); ;
                TextoTiempo.GetComponent<Text>().text = titulosTiempo[this.estadoPeriodo];
                this.slideTiempo.value = this.estadoPeriodo;

            }
        }

    }

    private void botonDerecho()
    {
        if (LeanTween.tweensRunning == 0)
        {
            if (this.estadoPeriodo < titulosPruebas.Count - 1)
            {
                this.estadoPeriodo++;
                LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x - 890, 0.25f).setEaseOutCubic();
                TextoTiempo.GetComponent<Text>().text = titulosTiempo[this.estadoPeriodo];
                this.slideTiempo.value = this.estadoPeriodo;
            }
        }
       

    }

    private void okBoton()
    {
        Debug.Log("jej");
    }

    private void regresarBoton()
    {
        Debug.Log("jej");
    }

    private void salir()
    {
        UnityEngine.XR.XRSettings.enabled = false;
        SceneManager.LoadScene("MenuMultimediaInteractivo");
    }




    private void Instance_seTocoBoton(object sender, EventArgs e)
    {

        if(soloUnComando())
        {
            if(componentBluetooth.Instance.dataRecived[2] == '1' )
            {
                botonIzquierdo();
            }
            else if(componentBluetooth.Instance.dataRecived[3] == '1')
            {
                botonDerecho();
            }
            else if (componentBluetooth.Instance.dataRecived[4] == '1')
            {
                okBoton();
            }
            else if (componentBluetooth.Instance.dataRecived[5] == '1')
            {
                regresarBoton();
            }
            else if (componentBluetooth.Instance.dataRecived[6] == '1')
            {
                salir();
            }

        }
    }

    void OnDisable()
    {
        componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
    }

    // Update is called once per frame

}
