using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

public class game_controller : MonoBehaviour
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
    public GameObject cuentaRegresiva;
    private int num_note;
    private bool game_finished;
    private songCharacter dato;
    private string[] canciones;
    private int indexSong;
    private TextMeshPro cancionEscoger;
    public GameObject beatAndPlane;
    public GameObject Score;

    [Serializable]
    private class songCharacter
    {
        public int beatTempo;
        public List<List<int>> song;
    }


    void CargaCancion(String song)
    {

       
       
        string Jsona = File.ReadAllText(Application.dataPath + "/songs/" + song + ".json");
        this.num_note = 0;
        Score.SetActive(true);
        dato = JsonConvert.DeserializeObject<songCharacter>(Jsona);
        int y_t = 1221;
        int x_t;
        for (int i = dato.song.Count-1; i >-1 ; i--)
        {
            x_t = 12;
            for (int j = 0; j < 12; j++)
            {
                if (dato.song[i][j] == 1)
                {
                    num_note += 1;
                    switch (j)
                    {
                        case 0:
                            Instantiate(nota_1, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(nota_2, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(nota_3, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(nota_4, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 4:
                            Instantiate(nota_5, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 5:
                            Instantiate(nota_6, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 6:
                            Instantiate(nota_7, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 7:
                            Instantiate(nota_8, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 8:
                            Instantiate(nota_9, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 9:
                            Instantiate(nota_10, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 10:
                            Instantiate(nota_11, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                        case 11:
                            Instantiate(nota_12, new Vector3(x_t, y_t, 14.8f), Quaternion.identity);
                            break;
                    }
                }
                x_t += 250;
            }
            y_t += 300;
        }

    }

    [Obsolete]
    void Start()
    {
        string tmpString;
        int index;
        this.indexSong = 0;
        this.canciones = Directory.GetFiles(Application.dataPath + "/songs/", "*.json");
        for(int i = 0; i<this.canciones.Length; i++)
        {
            tmpString = Path.GetFileName(this.canciones[i]);
            index = tmpString.IndexOf(".json");
            this.canciones[i] = tmpString.Substring(0, index);
        }
        this.cancionEscoger = cuentaRegresiva.transform.FindChild("background").FindChild("NombreCancion").GetComponent<TextMeshPro>();
        this.cancionEscoger.text = this.canciones[this.indexSong];
        
        this.game_finished = true;
    
       
    }

    public bool estadoJuego() {
        return game_finished;
    }
    public int consultarTemp()
    {
        return dato.beatTempo;
    }
    public void restarNotas()
    {
        this.num_note -= 1;
        if(num_note == 0)
        {
            game_finished = true;
            beatAndPlane.SetActive(false);
            Score.SetActive(false);
            Score.GetComponent<ScoreController>().reset();
            cuentaRegresiva.SetActive(true);
        }
    }

    


    [Obsolete]
    IEnumerator CuentaRegresiva(int i) 
    {
        TextMeshPro texto = cuentaRegresiva.transform.FindChild("background").FindChild("TextoCuenta").GetComponent<TextMeshPro>();
        texto.text = i.ToString();
        texto.fontSize = 50;
        yield return new WaitForSeconds(1.0f);

        if (i > 0)
        {
            StartCoroutine("CuentaRegresiva", i-1);
        }
        else
        {
            this.game_finished = false;
            beatAndPlane.SetActive(true);
            
            cuentaRegresiva.SetActive(false);
            cuentaRegresiva.transform.FindChild("background").FindChild("TituloCuadro").gameObject.SetActive(true);
            cuentaRegresiva.transform.FindChild("background").FindChild("NombreCancion").gameObject.SetActive(true);
            cuentaRegresiva.transform.FindChild("background").FindChild("Arrows").gameObject.SetActive(true);
            cuentaRegresiva.transform.FindChild("background").FindChild("TextoCuenta").gameObject.SetActive(false);
        }
        
    }

    void cambiarCancion(int i)
    {
        if(i == 1)
        {
            if (this.indexSong == this.canciones.Length - 1)
            {
                this.indexSong = 0;
            }
            else
            {
                this.indexSong += 1;
            }

        }
        else
        {
            if(this.indexSong == 0)
            {
                this.indexSong = this.canciones.Length - 1;
            }
            else
            {
                this.indexSong -= 1;
            }
        }

        this.cancionEscoger.text = this.canciones[this.indexSong];

    }

    [Obsolete]
    void OnGUI()
    {
        //Boton Medio
       
        if (Event.current.Equals(Event.KeyboardEvent("m")))
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
        if (Event.current.Equals(Event.KeyboardEvent("a")))
        {
            if(game_finished == true)
            {
                cambiarCancion(-1);
            }

         


        }

        //Boton Derecho

        if (Event.current.Equals(Event.KeyboardEvent("s")))
        {

            if (game_finished == true )
            {
                cambiarCancion(1);
            }
                


        }

    }


}
