using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_codeMultimedia : MonoBehaviour
{
    public partial class EscenasCultura
    {
        [JsonProperty("Escenas")]
        public List<Escena> Escenas { get; set; }
    }

    public partial class Escena
    {
        [JsonProperty("AssetName")]
        public string AssetName { get; set; }

        [JsonProperty("materials")]
        public List<Material> Materials { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }
    }

    public partial class Material
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("propierty")]
        public Propierty Propierty { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("typeAnimation", NullValueHandling = NullValueHandling.Ignore)]
        public string TypeAnimation { get; set; }
    }

    public partial class Propierty
    {
        [JsonProperty("dimension")]
        public Dimension Dimension { get; set; }

        [JsonProperty("fontSize")]
        public long FontSize { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("scale")]
        public Scale Scale { get; set; }
    }

    public partial class Dimension
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class Position
    {
        [JsonProperty("posX")]
        public long PosX { get; set; }

        [JsonProperty("posY")]
        public long PosY { get; set; }

        [JsonProperty("posZ")]
        public long PosZ { get; set; }
    }

    public partial class Scale
    {
        [JsonProperty("scaleX")]
        public long ScaleX { get; set; }

        [JsonProperty("scaleY")]
        public long ScaleY { get; set; }

        [JsonProperty("scaleZ")]
        public long ScaleZ { get; set; }
    }

    private const string projectId = "quickstart-1595792293378";
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com";
    public GameObject menuCatalogo;
    public GameObject ContentTitulos;
    public GameObject itemTitulos;
    int estadoPeriodo = 0;
    private EscenasCultura DatosEscenas;
    public GameObject cargarScene;
    public GameObject audioFondo;

    public GameObject instruccionMenu;

    [Obsolete]
    public IEnumerator consultarDatos()
    {
        UnityWebRequest www = UnityWebRequest.Get($"{databaseURL}/DataGame/Multimedia/Cultura.json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {

            Debug.Log("error");

        }
        else
        {
            try
            {
                Debug.Log(www.downloadHandler.text);
                this.DatosEscenas = JsonConvert.DeserializeObject<EscenasCultura>(www.downloadHandler.text);
                desplegarEscenas();
            }
            catch
            {
                Debug.Log("Error");
            }
        }
    }

    void Start()
    {
        StartCoroutine("consultarDatos");
    }

    [Obsolete]
    void desplegarEscenas()
    {

        UnityEngine.XR.XRSettings.enabled = true;
        componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
        GameObject item = null;
        for (int i = 0; i < this.DatosEscenas.Escenas.Count; i++)
        {
            item = Instantiate(itemTitulos);
            item.GetComponent<Text>().text = this.DatosEscenas.Escenas[i].Title;
            item.transform.parent = ContentTitulos.transform;
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.LeanSetPosZ(0);
        }


    }

    private bool soloUnComando()
    {
        int NumNotasExactas = 0;
        int notaExactaTocada = -1;
        for (int i = 0; i < 12; i++)
        {
            if (componentBluetooth.Instance.dataRecived[i] == '1')
            {
                NumNotasExactas++;
                notaExactaTocada = i;
            }
        }
        return NumNotasExactas == 1 && notaExactaTocada != -1;

    }




    private void botonIzquierdo()
    {
        if (LeanTween.tweensRunning == 0)
        {

            if (this.estadoPeriodo != 0)
            {
                this.estadoPeriodo--;

                LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x + 847.5f, 0.25f).setEaseOutCubic();

            }
          
        }

    }

    private void botonDerecho()
    {
        if (LeanTween.tweensRunning == 0)
        {
            if (this.estadoPeriodo < this.DatosEscenas.Escenas.Count - 1)
            {
                this.estadoPeriodo++;
                Debug.Log(ContentTitulos.transform.localPosition.x - 847.5f);
                LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x - 847.5f, 0.25f).setEaseOutCubic();
            }
           
        }

    }

     /* void OnGUI()
      {
          
          Event e = Event.current;
          if (e.isKey)
              if (e.keyCode == KeyCode.Space)
              {
                  cargarScene.GetComponent<cargarSceneCultura>().setEscenaSeleccionada(this.DatosEscenas.Escenas[this.estadoPeriodo]);
                  cargarScene.SetActive(true);
                  menuCatalogo.SetActive(false);
                  this.gameObject.SetActive(false);
              }
              else if (e.keyCode == KeyCode.RightArrow)
              {
                  if (LeanTween.tweensRunning == 0)
                  {
                      if (this.estadoPeriodo < this.DatosEscenas.Escenas.Count - 1)
                      {
                        this.estadoPeriodo++;
                        LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x - 847.5f, 0.25f).setEaseOutCubic();
                      }
                      else
                      {
                        this.estadoPeriodo = 0;
                        LeanTween.moveLocalX(ContentTitulos, Convert.ToSingle(-1255.644) + Convert.ToSingle(847.5f) , 0.25f).setEaseOutCubic();
                    
                      }
                  }

              }
              else if (e.keyCode == KeyCode.LeftArrow)
              {
                  if (LeanTween.tweensRunning == 0)
                  {
                 
                      if (this.estadoPeriodo != 0)
                      {
                         this.estadoPeriodo--;

                        LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x + 847.5f , 0.25f).setEaseOutCubic();

                      }
                      else
                      {

                        this.estadoPeriodo = this.DatosEscenas.Escenas.Count - 1;
                        LeanTween.moveLocalX(ContentTitulos, Convert.ToSingle(-6340.644) - Convert.ToSingle(847.5f), 0.25f).setEaseOutCubic();
                      }
                  }
              }
       }*/



    private void okBoton()
    {
        cargarScene.GetComponent<cargarSceneCultura>().setEscenaSeleccionada(this.DatosEscenas.Escenas[this.estadoPeriodo]);
        cargarScene.SetActive(true);
        menuCatalogo.SetActive(false);
        gameObject.SetActive(false);

    
    }

    [Obsolete]
    private void salir()
    {
        UnityEngine.XR.XRSettings.enabled = false;
        componentBluetooth.Instance.seTocoBoton -= Instance_seTocoBoton;
        SceneManager.LoadScene("MenuMultimediaInteractivo");
    }

    [Obsolete]
    private void Instance_seTocoBoton(object sender, EventArgs e)
    {

        if (soloUnComando() && gameObject.active)
        {
            if (componentBluetooth.Instance.dataRecived[2] == '1')
            {
                botonIzquierdo();
            }
            else if (componentBluetooth.Instance.dataRecived[3] == '1')
            {
                botonDerecho();
            }
            else if (componentBluetooth.Instance.dataRecived[4] == '1')
            {
                okBoton();
            }
            else if (componentBluetooth.Instance.dataRecived[6] == '1')
            {
                salir();
            }

        }
    }

    [Obsolete]
    void OnEnable()
    {
        componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
        audioFondo.GetComponent<AudioSource>().Play();
        instruccionMenu.SetActive(true);
 
}

    [Obsolete]
    void OnDisable()
    {
        componentBluetooth.Instance.seTocoBoton -= Instance_seTocoBoton;
        audioFondo.GetComponent<AudioSource>().Stop();
        instruccionMenu.SetActive(false);


    }

    // Update is called once per frame



}
