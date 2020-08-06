using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaController : MonoBehaviour
{
    // Start is called before the first frame update
    private float tempo;
    private ScoreController punt;
    public string id_Note;
    private bool EstadoJuego;
    private GameControl CodeGame;
    void Start()
    {
       CodeGame = GameObject.Find("Code_Control").GetComponent<GameControl>();
       tempo =  map(CodeGame.GetComponent<GameControl>().consultarTemp());
       punt = GameObject.Find("Score").GetComponent<ScoreController>();

        
    }

    float map(float temp_in)
    {

        return Convert.ToSingle(((temp_in - 100) * (0.35 - 0.15) / (300 - 100) + 0.15));

    }
    IEnumerator animacion()
    {
        punt.acertado();
        CodeGame.restarNotas();
        Animator effect = gameObject.GetComponent<Animator>();
        effect.Play("nota"+this.id_Note);
        yield return new WaitForSeconds(1.5f);
        
        Destroy(gameObject);
      

    }

    private bool estadoJuego()
    {
        //Si el juego no esta corriendo es true y si esta corriendo el false
        return GameObject.Find("Code_Control").GetComponent<GameControl>().estadoJuego();
    }

    [System.Obsolete]
    void Update()
    {


        if (estadoJuego() == false)
        {
            if (gameObject.transform.position.y < -0.5f)
            {
                CodeGame.restarNotas();
                punt.fallo();
                DestroyObject(gameObject);
            }
            else
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - tempo * Time.deltaTime, gameObject.transform.position.z);
            }
        }
    }

    public void colision()
    {
       
        StartCoroutine("animacion");
        
    }

    
}
