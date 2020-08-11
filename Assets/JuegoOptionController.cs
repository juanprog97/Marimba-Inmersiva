using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JuegoOptionController : MonoBehaviour
{

    private string UrlStorage;
    public GameObject debug;
    public GameObject Menu;
    public GameObject Info;
    public GameObject DataInfoRanking;
    public GameObject NameSong;
    public string estaSonando = "";
    private DataGame Datos;
    private int indexSong;
    private string AssetName = "canciones";
    public GameObject code_control;
    public GameObject ArCamera;
    public GameObject ImageTarget;
    public GameObject Lights;
    public GameObject Control;


    private const string projectId = "quickstart-1595792293378";
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/DataGame";

    public class Ranking
    {
        
        public string nPuntaje { get; set; }
        public string nUsuario { get; set; }

    }

    public class Song
    {
        public string autor { get; set; }
        public string informacion { get; set; }
        public string nombre { get; set; }
        public string punt_alto { get; set; }
        public List<Ranking> ranking { get; set; }
    }

    public class DataGame
    {
        public List<Song> Songs { get; set; }
    }

    [System.Obsolete]
    IEnumerator DescargarCanciones()
    {
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        // Create a storage reference from our storage service
        Firebase.Storage.StorageReference reference = storage.GetReferenceFromUrl("gs://quickstart-1595792293378.appspot.com/AssetsBundles/Musica/canciones");
        var task = reference.GetDownloadUrlAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        while (!Caching.ready)
            yield return null;

        using (WWW www = WWW.LoadFromCacheOrDownload(task.Result.ToString(), 2))
        {
            yield return www;
            if (www.error != null)
                throw new Exception("WWW download had an error:" + www.error);
            AssetBundle bundle = www.assetBundle;
            if (AssetName == "")
            {
                Instantiate(bundle.mainAsset);
                debug.GetComponent<Text>().text = "Primerif";
            }
            else
            {
                
                var cancionesDescargadas = Instantiate(bundle.LoadAsset(AssetName),gameObject.transform.GetComponent<Transform>());
                debug.GetComponent<Text>().text = "Segundoif";
            }
               
            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);

        }


        
    }

    [Obsolete]
    void reproducir(string nombreCancion)
    {
       

        if (this.estaSonando != "")
        {
            try
            {
                GameObject.Find("Canciones(Clone)").gameObject.transform.FindChild(this.estaSonando).GetComponent<AudioSource>().Stop();
                this.estaSonando = "";
            }
            catch
            {
                debug.GetComponent<Text>().text = "No se paro la cancion inicial";
            }
        }

        if (this.estaSonando == "")
        {
            try
            {
                GameObject.Find("Canciones(Clone)").gameObject.transform.FindChild(nombreCancion).GetComponent<AudioSource>().Play();
                this.estaSonando = nombreCancion;
            }
            catch
            {
                debug.GetComponent<Text>().text = "No se reproduce la cancion nueva";
            }
            
        }
    }

    [Obsolete]
    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine("DescargarCanciones");
    
       /* FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                debug.GetComponent<Text>().text = "Funciona";
                StartCoroutine("Start");
            }
            else
            {
                  debug.GetComponent<Text>().text = string.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus);
                  // Firebase Unity SDK is not safe to use here.
            }
        });*/
    }

    [Obsolete]
    public IEnumerator consultarDatos(int repo)
    {
        UnityWebRequest www = UnityWebRequest.Get($"{databaseURL}.json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            // EditorUtility.DisplayDialog("Response", "Error", "Ok");
            debug.GetComponent<Text>().text = "ERROR";
        }
        else
        {

            debug.GetComponent<Text>().text = "Ok";
            try
            {

                this.Datos = JsonConvert.DeserializeObject<DataGame>(www.downloadHandler.text);
                if(repo == 1)
                {
                    reproducir(Datos.Songs[indexSong].nombre);
                }
                this.Actualizar();



            }
            catch
            {
                debug.GetComponent<Text>().text = "Error Try";
            }
            

        }
    }


    [System.Obsolete]
    void Start()
    {
        debug.GetComponent<Text>().text = "Entro";
        indexSong = 0;
        StartCoroutine("consultarDatos",1);
       
        
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl($"{databaseURL}");
        FirebaseDatabase.DefaultInstance.GetReference("DataGame/Songs/").ValueChanged += HandleValueChanged; // unsubscribe from ValueChanged.
        

        /*  UnityWebRequest www = UnityWebRequest.Get($"{databaseURL}DataGame.json");
          yield return www.SendWebRequest();
          if (www.isNetworkError || www.isHttpError)
          {
              // EditorUtility.DisplayDialog("Response", "Error", "Ok");
                   debug.GetComponent<Text>().text =  "ERROR";
          }
          else
          {

              debug.GetComponent<Text>().text = "Ok";
              try
              {

                  this.Datos = JsonConvert.DeserializeObject<DataGame>(www.downloadHandler.text);
                  Actualizar();


              }
              catch
              {
                  debug.GetComponent<Text>().text = "Error Try";
              }


          }*/
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        StartCoroutine("consultarDatos",0);
        
    }

    public void cerrarInfo()
    {
        Info.SetActive(false);
    }

    [System.Obsolete]
    public void Actualizar()
    {
        NameSong.GetComponent<Text>().text = Datos.Songs[indexSong].nombre;
        Info.transform.FindChild("Container").FindChild("Text").GetComponent<Text>().text = Datos.Songs[indexSong].informacion;
        GameObject itemRank;
        debug.GetComponent<Text>().text = Datos.Songs[indexSong].ranking.Count.ToString();
        for (int i=0; i<15; i++)
        {
            itemRank = this.DataInfoRanking.transform.GetChild(i).gameObject;
            itemRank.transform.FindChild("nombre").GetComponent<Text>().text =  Datos.Songs[indexSong].ranking[i].nUsuario ;
            itemRank.transform.FindChild("puntaje").GetComponent<Text>().text = Datos.Songs[indexSong].ranking[i].nPuntaje ;
        }
        

    }

    [System.Obsolete]
    public void slideRight()
    {
       
        
        indexSong += 1;

        if (indexSong == Datos.Songs.Count)
        {
            indexSong = 0;
        }
        this.Actualizar();
        reproducir(Datos.Songs[indexSong].nombre);


    }

    [System.Obsolete]
    public void slideLeft()
    {
      
        indexSong -= 1;
        if (indexSong == -1)
        {
            indexSong = Datos.Songs.Count-1;
        }
        this.Actualizar();
        reproducir(Datos.Songs[indexSong].nombre);


    }


    [Obsolete]
    public void regresar()
    {
        Menu.SetActive(true);
        
        this.gameObject.SetActive(false);
    }

    public void info()
    {
        Info.SetActive(true);
    }


    public void Jugar()
    {
        UnityEngine.XR.XRSettings.enabled = true;
        Screen.orientation = ScreenOrientation.Landscape;
        ArCamera.SetActive(true);
        ImageTarget.SetActive(true);
        Lights.SetActive(true);
        Control.SetActive(true);
        code_control.GetComponent<GameControl>().iniciarJuego(Datos.Songs[indexSong].nombre.Replace(" ",""));
        gameObject.SetActive(false);

    }


    public void subirPuntaje(string usuario,int puntaje)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        Ranking nuevoRank = new Ranking();
        nuevoRank.nPuntaje = puntaje.ToString();
        nuevoRank.nUsuario = usuario;
        string usuario_temp;
        string puntaje_temp;
        if (puntaje > Int32.Parse(this.Datos.Songs[indexSong].punt_alto))
        {
            reference.Child("DataGame").Child("Songs").Child(indexSong.ToString()).Child("punt_alto").SetValueAsync(puntaje.ToString());
        }

         for (int i =0; i<15; i++)
         {
             if(Int32.Parse(this.Datos.Songs[indexSong].ranking[i].nPuntaje) < puntaje)
             {
                 this.Datos.Songs[indexSong].ranking.Insert(i, nuevoRank);
                 this.Datos.Songs[indexSong].ranking.RemoveAt(this.Datos.Songs[indexSong].ranking.Count-1);
                 break;
             }

         }

         for (int i = 0; i<15; i++)
         {
            usuario_temp = this.Datos.Songs[indexSong].ranking[i].nUsuario;
            puntaje_temp = this.Datos.Songs[indexSong].ranking[i].nPuntaje; 
            reference.Child("DataGame").Child("Songs").Child(indexSong.ToString()).Child("ranking").Child(i.ToString()).Child("nPuntaje").SetValueAsync(puntaje_temp.ToString());
            reference.Child("DataGame").Child("Songs").Child(indexSong.ToString()).Child("ranking").Child(i.ToString()).Child("nUsuario").SetValueAsync(usuario_temp);
        }


    }

    
}
