using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuConfigurationController : MonoBehaviour
{
    // Start is called before the first frame update
    private TMPro.TextMeshProUGUI textMesh;
    private Image state;
    public Sprite check;
    public Sprite wrong;

    public GameObject managet_t;

   // private bool state_menuConfiguration = true;
    private GameObject animationSearch;
    private GameObject reload;
    private GameObject stateObject;
    public GameObject Bluetooth_t;
    private GameObject logo_t;
    private BluetoothController state_bluetooth;
    private string info_state;
    [System.Obsolete]
    void Start()
    {

        this.textMesh = gameObject.transform.FindChild("Panel").transform.FindChild("Notify").GetComponent<TMPro.TextMeshProUGUI>();
        this.stateObject = gameObject.transform.FindChild("Panel").transform.FindChild("State").gameObject;
        this.state = gameObject.transform.FindChild("Panel").transform.FindChild("State").GetComponent<Image>();
        this.logo_t = gameObject.transform.FindChild("Panel").transform.FindChild("Comunication").gameObject;
        this.animationSearch = gameObject.transform.FindChild("Panel").transform.FindChild("AnimacionBusqueda").gameObject;
        this.reload = gameObject.transform.FindChild("Panel").transform.FindChild("reloadSearch").gameObject;
        this.textMesh.text = "Buscando Sincronizacion...";
        this.state_bluetooth = Bluetooth_t.GetComponent<BluetoothController>();
        animationSearch.SetActive(true);
        
        
    }

    [System.Obsolete]
    public void seeConnectionBluetooth(int info)
    {
        
        if (info == 1)
        {
            StartCoroutine(waitAnimacion(2));

         
            managet_t.GetComponent<ControllerMenu>().invokeMenuPrincipal();

        }
        else if (info == -1)
        {
            StartCoroutine(waitAnimacion(3));
        }
        else if (info == -2)
        {
            StartCoroutine(waitAnimacion(3));
        }

       

    }

    // Update is called once per frame
    [System.Obsolete]
    private IEnumerator waitAnimacion(int option)
    {
        //show animate out animation
        if (option == 1)
        {
            this.reload.GetComponent<Animator>().Play("Pressed");
            yield return new WaitForSeconds(2);
            this.textMesh.text = "Buscando Sincronizacion...";
            this.reload.SetActive(false);
            this.animationSearch.SetActive(true);
            state_bluetooth.reconnect();
        }

        if (option == 2)
        {
  
            this.animationSearch.SetActive(false);
            this.logo_t.SetActive(false);
            this.stateObject.SetActive(true);
            state.sprite = this.check;
            textMesh.text = "Sincronizacion realizada correctamente";
            yield return new WaitForSeconds(5);
            managet_t.GetComponent<ControllerMenu>().invokeMenuPrincipal();
            // this.Canv1_t.SetActive(false);
            // this.Ar_t.SetActive(true);
            ///this.Target_t.SetActive(true);
            // this.Canv_t.SetActive(true);

        }



        if (option == 3)
        {
            
            this.animationSearch.SetActive(false);
            this.logo_t.SetActive(false);
            this.stateObject.SetActive(true);
            this.state.sprite = this.wrong;
            textMesh.text = "No se pudo sincronizar la conexion";
            //this.animationSearch.SetActive(false);
            //this.state.SetActive(true);
            //this.state.GetComponent<Image>().sprite = this.wrong;
            //this.setText.GetComponent<TMPro.TextMeshProUGUI>().text = "No se pudo sincronizar la conexion.";
            yield return new WaitForSeconds(4);
            this.logo_t.SetActive(true);
            this.stateObject.SetActive(false);
            this.reload.SetActive(true);
         


            

            // this.setText.GetComponent<TMPro.TextMeshProUGUI>().text = "Vuelva intentar la sincronizacion";
            // this.state.SetActive(false);
            //this.reload.SetActive(true);


        }


        //load the scene we want
    }

    [System.Obsolete]
    public void reset()
    {
       StartCoroutine(waitAnimacion(1));
    }
}

