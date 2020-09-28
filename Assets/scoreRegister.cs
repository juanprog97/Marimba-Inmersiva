using UnityEngine;
using UnityEngine.UI;


public class scoreRegister : MonoBehaviour
{
   
    public GameObject puntaje;
    public GameObject nUsuario;
    public GameObject juegoPuntaje;
    public int nPuntaje;


    void OnEnable()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
   


    public void rellenar(int nPuntaje)
    {
        this.nPuntaje = nPuntaje;
        puntaje.GetComponent<Text>().text = nPuntaje.ToString();
    }



    public void registrar()
    {
        juegoPuntaje.GetComponent<JuegoOptionController>().subirPuntaje(nUsuario.GetComponent<InputField>().text, nPuntaje);
        juegoPuntaje.SetActive(true);
        nUsuario.GetComponent<InputField>().text = "";
        gameObject.SetActive(false);

    }


    // Update is called once per frame
    
}
