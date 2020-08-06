using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuConfigurationController : MonoBehaviour
{
    // Start is called before the first frame update
    private TMPro.TextMeshProUGUI textMesh;
    private Image state;
    public Sprite check;
    public Sprite wrong;
    private GameObject animationSearch;
    private GameObject reload;
    private GameObject stateObject;
    private GameObject logo_t;
    private GameObject play_app;
    [System.Obsolete]
    void Start()
    {
        UnityEngine.XR.XRSettings.enabled = false;
        Screen.orientation = ScreenOrientation.Portrait;
        this.textMesh = gameObject.transform.FindChild("Panel").transform.FindChild("Notify").GetComponent<TMPro.TextMeshProUGUI>();
        this.stateObject = gameObject.transform.FindChild("Panel").transform.FindChild("State").gameObject;
        this.state = gameObject.transform.FindChild("Panel").transform.FindChild("State").GetComponent<Image>();
        this.logo_t = gameObject.transform.FindChild("Panel").transform.FindChild("Comunication").gameObject;
        this.animationSearch = gameObject.transform.FindChild("Panel").transform.FindChild("AnimacionBusqueda").gameObject;
        this.reload = gameObject.transform.FindChild("Panel").transform.FindChild("reloadSearch").gameObject;
        this.play_app= gameObject.transform.FindChild("Panel").transform.FindChild("playApp").gameObject;
        this.textMesh.text = "Buscando Sincronizacion...";
        animationSearch.SetActive(true);
        seeConnectionBluetooth();
    }

    [System.Obsolete]
    public void seeConnectionBluetooth()
    {
        bool state = componentBluetooth.Instance.IsConnected;
        if (state == true)
        {
            StartCoroutine(waitAnimacion(2));

        }
        else
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
            this.stateObject.SetActive(false);
            this.textMesh.text = "Buscando Sincronizacion...";
            this.reload.SetActive(false);
            this.logo_t.SetActive(true);
            this.animationSearch.SetActive(true);
            yield return new WaitForSeconds(3);
            componentBluetooth.Instance.reconnect();
            seeConnectionBluetooth();
        }

        if (option == 2)
        {
  
            this.animationSearch.SetActive(false);
            this.logo_t.SetActive(false);
            this.stateObject.SetActive(true);
            state.sprite = this.check;
            textMesh.text = "Sincronizacion realizada correctamente";
            yield return new WaitForSeconds(3);
            this.play_app.SetActive(true);


        }
        if (option == 3)
        {
            
            this.animationSearch.SetActive(false);
            this.logo_t.SetActive(false);
            this.stateObject.SetActive(true);
            this.state.sprite = this.wrong;
            textMesh.text = "No se pudo sincronizar la conexion";
            yield return new WaitForSeconds(3);
            this.reload.SetActive(true);
        }
    }
    public void moveMenuPrincipal()
    {
       
        SceneManager.LoadScene("PrincipalMenu");
    }

    [System.Obsolete]
    public void reset()
    {
        
        StartCoroutine(waitAnimacion(1));
    }
}

