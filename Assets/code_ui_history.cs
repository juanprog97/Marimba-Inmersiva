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

    public partial class Escenas
    {
        [JsonProperty("historia")]
        public List<Historia> Historia { get; set; }
    }

    public partial class Historia
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("AssetName")]
        public string AssetName { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("materials")]
        public List<Material> Materials { get; set; }
    }

    public partial class Material
    {
        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("scale")]
        public Scale Scale { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("typeAnimation")]
        public string TypeAnimation { get; set; }
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
    public GameObject boton_izquierdo;
    public GameObject boton_derecho;
   // private componentBluetooth escuchaComando;
    public GameObject ContentTitulos;
    public GameObject itemTitulos;
    public Text TextoTiempo;
    public Slider slideTiempo;
    int estadoPeriodo = 0;
    private Escenas DatosEscenas;





    public IEnumerator consultarDatos()
    {
        UnityWebRequest www = UnityWebRequest.Get($"{databaseURL}/DataGame/Multimedia.json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
      
            Debug.Log("error");
        
        }
        else
        {
            try
            {
                this.DatosEscenas = JsonConvert.DeserializeObject<Escenas>(www.downloadHandler.text);
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

        UnityEngine.XR.XRSettings.enabled = true;
        //componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
        GameObject item = null ;
        this.slideTiempo.minValue = 0;
        this.slideTiempo.maxValue = this.DatosEscenas.Historia.Count-1 ;
        this.slideTiempo.value = 0;
        for (int i = 0; i< this.DatosEscenas.Historia.Count; i++)
        {
            item = Instantiate(itemTitulos);
            item.GetComponent<Text>().text = this.DatosEscenas.Historia[i].Title;
            item.transform.parent = ContentTitulos.transform;
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.LeanSetPosZ(0);
        }
        TextoTiempo.GetComponent<Text>().text = this.DatosEscenas.Historia[this.estadoPeriodo].Time.ToString();


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
                LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x + 890, 0.25f).setEaseOutCubic(); ;
                TextoTiempo.GetComponent<Text>().text = this.DatosEscenas.Historia[this.estadoPeriodo].Time.ToString();
                this.slideTiempo.value = this.estadoPeriodo;

            }
        }

    }

    private void botonDerecho()
    {
        if (LeanTween.tweensRunning == 0)
        {
            if (this.estadoPeriodo < this.DatosEscenas.Historia.Count - 1)
            {
                this.estadoPeriodo++;
                LeanTween.moveLocalX(ContentTitulos, ContentTitulos.transform.localPosition.x - 890, 0.25f).setEaseOutCubic();
                TextoTiempo.GetComponent<Text>().text = this.DatosEscenas.Historia[this.estadoPeriodo].Time.ToString();
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
      //  componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
    }

    // Update is called once per frame

}
