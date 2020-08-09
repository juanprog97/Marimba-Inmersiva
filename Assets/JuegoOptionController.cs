using Firebase;
using Firebase.Unity.Editor;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JuegoOptionController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject debug;
    public GameObject Menu;
    public GameObject Info;
    public GameObject DataInfoRanking;
    public GameObject NameSong;
    private DataGame Datos;
    private int indexSong;


    private const string projectId = "quickstart-1595792293378";
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";

    public class Ranking
    {
        public int nPuntaje { get; set; }
        public string nUsuario { get; set; }
    }

    public class Song
    {
        public string autor { get; set; }
        public string informacion { get; set; }
        public string nombre { get; set; }
        public int punt_alto { get; set; }
        public List<Ranking> ranking { get; set; }
    }

    public class DataGame
    {
        public List<Song> Songs { get; set; }
    }

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
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
        });
    }

    [System.Obsolete]
    IEnumerator Start()
    {
        debug.GetComponent<Text>().text = "Entro";
        indexSong = 0;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl($"{databaseURL}");
        UnityWebRequest www = UnityWebRequest.Get($"{databaseURL}DataGame.json");
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


        }
    }






    public void cerrarInfo()
    {
        Info.SetActive(false);
    }

    [System.Obsolete]
    private void Actualizar()
    {
        NameSong.GetComponent<Text>().text = Datos.Songs[indexSong].nombre;
        Info.transform.FindChild("Container").FindChild("Text").GetComponent<Text>().text = Datos.Songs[indexSong].informacion;
        GameObject itemRank;
        debug.GetComponent<Text>().text = Datos.Songs[indexSong].ranking.Count.ToString();
        for (int i=0; i<15; i++)
        {
            itemRank = this.DataInfoRanking.transform.GetChild(i).gameObject;
            itemRank.transform.FindChild("nombre").GetComponent<Text>().text =  Datos.Songs[indexSong].ranking[i].nUsuario ;
            itemRank.transform.FindChild("puntaje").GetComponent<Text>().text = Datos.Songs[indexSong].ranking[i].nPuntaje.ToString() ;
        }
    }

    [System.Obsolete]
    public void slideRight()
    {
        indexSong += 1;
        if(indexSong == Datos.Songs.Count)
        {
            indexSong = 0;
        }
        this.Actualizar();
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
    }

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
        Debug.Log("Ed");
    }

    
}
