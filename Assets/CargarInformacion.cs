using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CargarInformacion : MonoBehaviour { 

    private const string projectId = "quickstart-1595792293378" ;
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/" ;
    private DataGame DatosRanking ;
    
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

    public DataGame getDataGame()
    {
        return this.DatosRanking;
    }

    void Awake()
    {
       FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
               // debug.GetComponent<Text>().text = "Funciona";
               StartCoroutine("Start");
           }
            else
            {
              /*  debug.GetComponent<Text>().text = string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus);
                // Firebase Unity SDK is not safe to use here.*/
            }
        });
    }

    IEnumerator Start()
    {
       // debug.GetComponent<Text>().text = "Entro";
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl($"{databaseURL}");
        UnityWebRequest www = UnityWebRequest.Get($"{databaseURL}DataGame.json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            // EditorUtility.DisplayDialog("Response", "Error", "Ok");
       //     debug.GetComponent<Text>().text =  "ERROR";
        }
        else
        {

          //  debug.GetComponent<Text>().text = "Ok";
            try
            {

                this.DatosRanking = JsonConvert.DeserializeObject<DataGame>(www.downloadHandler.text);
             //   debug.GetComponent<Text>().text = DatosRanking.Songs.Count.ToString();


            }
            catch
            {
               // debug.GetComponent<Text>().text = "Error Try";
            }


        }
    }
    // Update is called once per frame
}
