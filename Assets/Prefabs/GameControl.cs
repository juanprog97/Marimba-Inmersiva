using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.Linq;

public class GameControl : MonoBehaviour
{
    public GameObject nota_1;
    public GameObject nota_2;
    public GameObject nota_3;
    public GameObject nota_4;
    public GameObject nota_5;
    public GameObject nota_6;
    public GameObject nota_7;
    public GameObject nota_8;
    public GameObject nota_9;
    public GameObject nota_10;
    public GameObject nota_11;
    public GameObject nota_12;
    public GameObject debug;
    public int t_select;
    public int t_right;
    public int t_left;
    public GameObject cuentaRegresiva;
    private int num_note;
    private bool game_finished;
    private songCharacter dato;
    private int indexSong;
    private TextMeshPro cancionEscoger;
    private TextMeshPro de;
    public GameObject beatAndPlane;
    public GameObject Score;
    public GameObject GameParent;
    private Datagame dataGame;
    private TextMeshPro textoCuenta;
    public GameObject background;
    private const string projectId = "quickstart-1595792293378";
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";
    public  event EventHandler PUSH;

    [Serializable]
    public class songCharacter
    {
        public int beatTempo { get; set; }
        public List<List<int>> song { get; set; }
    }
    [Serializable]
    public class Ranking
    {
        public int nPuntaje { get; set; }
        public string nUsuario { get; set; }
    }

    public class Song
    {
        public string autor { get; set; }
        public string nombre { get; set; }
        public int punt_alto { get; set; }
        public List<Ranking> ranking { get; set; }
    }

    public class Datagame
    {
        public List<Song> Songs { get; set; }
    }

    [Obsolete]
    private IEnumerator CargarCancion(string song)
    {
        string path =  song + ".json"; 
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        // Create a storage reference from our storage service
        Firebase.Storage.StorageReference reference = storage.GetReferenceFromUrl("gs://quickstart-1595792293378.appspot.com/songs/" + path);
        var task = reference.GetDownloadUrlAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        Debug.Log(task.Result.ToString());
        UnityWebRequest www = UnityWebRequest.Get(task.Result.ToString());
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {

            de.text = "ERROR";
        }
        else
        {
            float PostZ = -0.14f;
        
            dato = JsonConvert.DeserializeObject<songCharacter>(www.downloadHandler.text);
            this.num_note = 0;
            Score.SetActive(true);
            float y_t = 1.0f;
            int x_t;
            Transform parent = GameParent.GetComponent<Transform>();
            for (int i = dato.song.Count - 1; i > -1; i--)
            {
                x_t = 0; 
                for (int j = 0; j < 12; j++)
                {
                    if (dato.song[i][j] == 1)
                    {
                        num_note += 1;
                        
                        switch (j)
                        {
                           
                            case 0:
                                Instantiate(nota_1, new Vector3(nota_1.transform.position.x+0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 1:
                                Instantiate(nota_2, new Vector3(nota_2.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);        
                                break;
                            case 2:
                                Instantiate(nota_3, new Vector3(nota_3.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 3:
                                Instantiate(nota_4, new Vector3(nota_4.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 4:
                                Instantiate(nota_5, new Vector3(nota_5.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 5:
                                Instantiate(nota_6, new Vector3(nota_6.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 6:
                                Instantiate(nota_7, new Vector3(nota_7.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 7:
                                Instantiate(nota_8, new Vector3(nota_8.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 8:
                                Instantiate(nota_9, new Vector3(nota_9.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 9:
                                Instantiate(nota_10, new Vector3(nota_10.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 10:
                                Instantiate(nota_11, new Vector3(nota_11.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                            case 11:
                                Instantiate(nota_12, new Vector3(nota_12.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
                                break;
                        }
                    }
                    x_t++;
                }
                y_t += 0.16f;
            }
            background.transform.FindChild("TituloCuadro").gameObject.SetActive(false);
            background.transform.FindChild("NombreCancion").gameObject.SetActive(false);
            background.transform.FindChild("Arrows").gameObject.SetActive(false);
            background.transform.FindChild("TextoCuenta").gameObject.SetActive(true);

            StartCoroutine("CuentaAtras", 3);
        }
    }

    [Obsolete]
    void Awake()
    {
        de = debug.transform.GetComponent<TextMeshPro>();
        cancionEscoger = cuentaRegresiva.transform.GetComponent<TextMeshPro>();
        textoCuenta = background.transform.FindChild("TextoCuenta").GetComponent<TextMeshPro>();
        textoCuenta.fontSize = 50;

        /*Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
               
                Start();
            }
            else
            {
                de.text =  System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus);
                // Firebase Unity SDK is not safe to use here.
            }
        });*/

    }

    public void izquierda()
    {
        cambiarCancion(-1);
    }

    public void derecha()
    {
        cambiarCancion(1);
    }

    public void enter()
    {
        if(game_finished == true)
        {
            StartCoroutine("CargarCancion", this.dataGame.Songs[indexSong].nombre);
        }
        else
        {
            PUSH?.Invoke(this, EventArgs.Empty);
        }
    }

    IEnumerator Start()
    {

         FirebaseApp.DefaultInstance.SetEditorDatabaseUrl($"{databaseURL}");
         this.dataGame = new Datagame();
         this.game_finished = true;
         UnityWebRequest www = UnityWebRequest.Get($"{databaseURL}DataGame.json");
         yield return www.SendWebRequest();
         if (www.isNetworkError || www.isHttpError)
         {
             // EditorUtility.DisplayDialog("Response", "Error", "Ok");
             de.text = "ERROR";
         }
         else
         {
             
            //EditorUtility.DisplayDialog("Response", www.downloadHandler.text, "Ok");
            try
            {
                de.text = www.downloadHandler.text;
             
                this.dataGame = JsonConvert.DeserializeObject<Datagame>(www.downloadHandler.text);
                this.indexSong = 0;
                de.text = dataGame.Songs.Count().ToString();
                cancionEscoger.text = dataGame.Songs[indexSong].nombre;
            
                
            }
            catch
            {
                de.text = "Se Jodio";
            }
            
            
        }
         
    }

    public bool estadoJuego()
    {
        return game_finished;
    }
    public int consultarTemp()
    {
        try
        {
            return dato.beatTempo;
        }
        catch
        {
            return 0;
        }
    }
    public void restarNotas()
    {
        this.num_note -= 1;
        if (num_note == 0)
        {
            game_finished = true;
            beatAndPlane.SetActive(false);
            Score.SetActive(false);
            background.SetActive(true);
            StartCoroutine("mostrarPuntaje");

            cuentaRegresiva.SetActive(true);
        }
    }

    [System.Obsolete]
    IEnumerator mostrarPuntaje()
    {
        background.transform.FindChild("TituloCuadro").GetComponent<TextMeshPro>().text = "Puntaje";
        background.transform.FindChild("NombreCancion").GetComponent<TextMeshPro>().text = Score.GetComponent<ScoreController>().getPuntaje().ToString();
        yield return new WaitForSeconds(5.0f);
        background.transform.FindChild("TituloCuadro").GetComponent<TextMeshPro>().text = "Escoge tu canción";
        background.transform.FindChild("NombreCancion").GetComponent<TextMeshPro>().text = dataGame.Songs[this.indexSong].nombre;
        Score.GetComponent<ScoreController>().reset();
    }


    [Obsolete]
    IEnumerator CuentaAtras(int i)
    {

        textoCuenta.text = i.ToString();
        
        yield return new WaitForSeconds(1.0f);

        if (i > 0)
        {
            StartCoroutine("CuentaAtras", i - 1);
        }
        else
        {
            
            

            background.SetActive(false);
            beatAndPlane.SetActive(true);
            background.transform.FindChild("TituloCuadro").gameObject.SetActive(true);
            background.transform.FindChild("NombreCancion").gameObject.SetActive(true);
            background.transform.FindChild("Arrows").gameObject.SetActive(true);
            background.transform.FindChild("TextoCuenta").gameObject.SetActive(false);
            this.game_finished = false;
        }

    }

    void cambiarCancion(int i)
    {
        if (i == 1)
        {
            this.indexSong += 1;
            if (this.indexSong > dataGame.Songs.Count()-1)
            {
                this.indexSong = 0;
            }
            

        }
        else
        {
            this.indexSong -= 1;
            if (this.indexSong < 0)
            {
                this.indexSong = dataGame.Songs.Count() - 1;
            }
        
        }
        cancionEscoger.text = dataGame.Songs[this.indexSong].nombre;
        

    }


    //[Obsolete]
    /*void Update()
    {

        /*string command = componentBluetooth.Instance.dataRecived;
        if (command[t_select] == '1')
        {

            if (this.game_finished == true)
            {
                CargaCancion(this.canciones[this.indexSong]);
                cuentaRegresiva.transform.FindChild("background").FindChild("TituloCuadro").gameObject.SetActive(false);
                cuentaRegresiva.transform.FindChild("background").FindChild("NombreCancion").gameObject.SetActive(false);
                cuentaRegresiva.transform.FindChild("background").FindChild("Arrows").gameObject.SetActive(false);
                cuentaRegresiva.transform.FindChild("background").FindChild("TextoCuenta").gameObject.SetActive(true);
                StartCoroutine("CuentaRegresiva", 3);
            }



        }
        //Boton izquierdo
        if (command[t_left] == '1')
        {
            if (game_finished == true)
            {
                cambiarCancion(-1);
            }



          
        }

        //Boton Derecho

        if (command[t_right] == '1')
        {

            if (game_finished == true)
            {
                cambiarCancion(1);
            }



        }

    }*/


            }
