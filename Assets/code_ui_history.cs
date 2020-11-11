using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine.Networking;
using UnityEditor;

public class code_ui_history : MonoBehaviour
{

    public partial class Historia
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

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

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

        [JsonProperty("typeAnimation")]
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
   // private componentBluetooth escuchaComando;
    public GameObject ContentTitulos;
    public GameObject itemTitulos;
    public Text TextoTiempo;
    public Slider slideTiempo;
    int estadoPeriodo = 0;
    private Historia DatosEscenas;
    public GameObject cargarScene;
    public List<Tuple<long,int>> ordenPeriodo ;



    public void ordenarPeriodos()
    {
        ordenPeriodo = new List<Tuple<long, int>>();
        for (int i = 0; i < this.DatosEscenas.Escenas.Count; i++)
        {
            Tuple<long, int> data = new Tuple<long, int>(this.DatosEscenas.Escenas[i].Order,i);
            this.ordenPeriodo.Add(data);
        }
        this.ordenPeriodo.Sort();
        Debug.Log(ordenPeriodo[0].Item1);
    }

    public IEnumerator consultarDatos()
    {
        UnityWebRequest www = UnityWebRequest.Get($"{databaseURL}/DataGame/Multimedia/Historia.json");
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
                this.DatosEscenas = JsonConvert.DeserializeObject<Historia>(www.downloadHandler.text);
                ordenarPeriodos();
                desplegarPeriodos();
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
    void desplegarPeriodos()
    {

       // UnityEngine.XR.XRSettings.enabled = true;
       // componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
        GameObject item = null ;
        this.slideTiempo.minValue = 0;
        this.slideTiempo.maxValue = this.DatosEscenas.Escenas.Count-1 ;
        this.slideTiempo.value = 0;
        for (int i = 0; i< ordenPeriodo.Count ; i++)
        {
            item = Instantiate(itemTitulos);
            item.GetComponent<Text>().text = this.DatosEscenas.Escenas[ordenPeriodo[i].Item2].Title;
            item.transform.parent = ContentTitulos.transform;
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.LeanSetPosZ(0);
        }
        TextoTiempo.GetComponent<Text>().text = this.DatosEscenas.Escenas[ordenPeriodo[this.estadoPeriodo].Item2].Time.ToString();


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




    private void botonIzquierdo()
    {
        if (LeanTween.tweensRunning == 0)
        {
            if (this.estadoPeriodo != 0)
            {
                this.estadoPeriodo--;
                LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x + 847.5f, 0.25f).setEaseOutCubic(); ;
                TextoTiempo.GetComponent<Text>().text = this.DatosEscenas.Escenas[ordenPeriodo[this.estadoPeriodo].Item2].
                    Time.ToString();
                this.slideTiempo.value = this.estadoPeriodo;

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
                LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x - 847.5f, 0.25f).setEaseOutCubic();
                TextoTiempo.GetComponent<Text>().text = this.DatosEscenas.Escenas[ordenPeriodo[this.estadoPeriodo].Item2].
                    Time.ToString();
                this.slideTiempo.value = this.estadoPeriodo;
            }
        }
  
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.keyCode == KeyCode.Space)
            {
                cargarScene.GetComponent<cargarScene>().setEscenaSeleccionada(this.DatosEscenas.Escenas[ordenPeriodo[this.estadoPeriodo].Item2]);
                cargarScene.SetActive(true);
                menuCatalogo.SetActive(false);
                this.gameObject.SetActive(false);
            }
            if (e.keyCode == KeyCode.RightArrow)
            {
                if (LeanTween.tweensRunning == 0)
                {
                    if (this.estadoPeriodo < this.DatosEscenas.Escenas.Count - 1)
                    {
                        this.estadoPeriodo++;
                        LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x - 847.5f, 0.25f).setEaseOutCubic();
                        TextoTiempo.GetComponent<Text>().text = this.DatosEscenas.Escenas[ordenPeriodo[this.estadoPeriodo].Item2].
                            Time.ToString();
                        this.slideTiempo.value = this.estadoPeriodo;
                    }
                }

            }
            if (e.keyCode == KeyCode.LeftArrow)
            {
                if (LeanTween.tweensRunning == 0)
                {
                    if (this.estadoPeriodo != 0)
                    {
                        this.estadoPeriodo--;
                        LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x + 847.5f, 0.25f).setEaseOutCubic(); ;
                        TextoTiempo.GetComponent<Text>().text = this.DatosEscenas.Escenas[ordenPeriodo[this.estadoPeriodo].Item2].
                            Time.ToString();
                        this.slideTiempo.value = this.estadoPeriodo;

                    }
                }

            }

        }
    }



    private void okBoton()
    {
        
    }

    private void regresarBoton()
    {
        Debug.Log("jej");
    }

    private void salir()
    {
     //   UnityEngine.XR.XRSettings.enabled = false;
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

   /* void OnDisable()
    {
        componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
    }*/

    // Update is called once per frame

}
