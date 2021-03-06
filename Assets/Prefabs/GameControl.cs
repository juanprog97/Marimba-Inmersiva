﻿using System.Collections;
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
using UnityEngine.SceneManagement;

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
    public GameObject cuentaRegresiva;
    private int num_note;
    private bool game_finished = true;
    private songCharacter dato;
    private TextMeshPro de;
    public GameObject beatAndPlane;
    public GameObject Score;
    public GameObject GameParent;
    private TextMeshPro textoCuenta;
    public GameObject background;
    public event EventHandler PUSH;
    private string cancionSeleccionada;

    public GameObject controlVuforia;
    private ScriptTracking estadoTracking;

  
    private componentBluetooth escuchaComando;


    public GameObject ScoreMenu;
    public GameObject ArCamera;
    public GameObject ImageTarget;
    public GameObject Light;

    public int prueba = 0;

    [Serializable]
    public class songCharacter
    {
        public int beatTempo { get; set; }
        public List<List<int>> song { get; set; }
    }
   

    [Obsolete]
    private IEnumerator CargarCancion(string song)
    {
        string path = song + ".json";
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
            Debug.LogWarning(" is Network error : " + www.isNetworkError);
            Debug.LogWarning(" is HTTP error : " + www.isHttpError);
            Debug.LogWarning(task.Result.ToString());
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
                                Instantiate(nota_1, new Vector3(nota_1.transform.position.x + 0.1f, y_t, PostZ), Quaternion.identity, parent);
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
       
            StartCoroutine("CuentaAtras", 3);
        }
    }




    /*public void enter()
    {
        
        if (game_finished == true)
        {
            textoCuenta.text = "";
            textoCuenta.fontSize = 50;
            GameParent.SetActive(true);
            
            StartCoroutine("CargarCancion", this.cancionSeleccionada);
        }
        else
        {
            PUSH?.Invoke(this, EventArgs.Empty);
        }
       
        
    }*/


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
            StartCoroutine("mostrarPuntaje");

        }
    }

    [Obsolete]
    void Awake()
    {
        UnityEngine.XR.XRSettings.enabled = true;
        
    }
    [System.Obsolete]
   
 
    void OnEnable()
    {
        estadoTracking = controlVuforia.GetComponent<ScriptTracking>();
        componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
        Screen.orientation = ScreenOrientation.Landscape;
        textoCuenta = background.transform.FindChild("TextoCuenta").GetComponent<TextMeshPro>();
        textoCuenta.fontSize = 20;
        textoCuenta.text = "pulse una tecla para comenzar ";
       

    }
    void OnDisable()
    {
     
        componentBluetooth.Instance.seTocoBoton -= Instance_seTocoBoton;
       
    }

    private void Instance_seTocoBoton(object sender, EventArgs e)
    {
        if(estadoTracking.getEstado())
        {
            if (game_finished == true)
            {
                textoCuenta.text = "";
                textoCuenta.fontSize = 50;
                GameParent.SetActive(true);
                StartCoroutine("CargarCancion", this.cancionSeleccionada);
            }
            else
            {
                PUSH?.Invoke(this, EventArgs.Empty);
            }
        }
       
    }

   
   /* public void EsperandoComando(object sender, EventArgs e)
    {
        
         if (game_finished == true)
         {
             textoCuenta.text = "";
             textoCuenta.fontSize = 50;
             GameParent.SetActive(true);
             game_finished = false;
             StartCoroutine("CargarCancion", this.cancionSeleccionada);
         }
         else
         {
             PUSH?.Invoke(this, EventArgs.Empty);
         }
    }*/


    [Obsolete]
    IEnumerator mostrarPuntaje()
     {
        beatAndPlane.SetActive(false);


        //  background.transform.FindChild("TituloCuadro").GetComponent<TextMeshPro>().text = "Puntaje";
        //  background.transform.FindChild("NombreCancion").GetComponent<TextMeshPro>().text = Score.GetComponent<ScoreController>().getPuntaje().ToString();
       // 
        yield return new WaitForSeconds(3.0f);
        background.SetActive(true);

        UnityEngine.XR.XRSettings.enabled = false;
        ArCamera.SetActive(false);
        ImageTarget.SetActive(false);
        Light.SetActive(false);
 
        ScoreMenu.SetActive(true);

        ScoreMenu.GetComponent<scoreRegister>().rellenar(Score.GetComponent<ScoreController>().getPuntaje());

        Score.SetActive(false);
        Score.GetComponent<ScoreController>().reset();
        
        componentBluetooth.Instance.seTocoBoton -= Instance_seTocoBoton;
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
            yield return new WaitForSeconds(1.0f);
            game_finished = false;
            

        }

    }

    public void iniciarJuego(string nombreCancion) {
        this.cancionSeleccionada = nombreCancion;
       
    }


}

    //[Obsolete]
    /*void Update()
    {

        /*string command = componentBluetooth.Instance.dataRecived;
        


     }*/
