using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaController : MonoBehaviour
{
    // Start is called before the first frame update
    private int tempo;
    private ScoreController punt;
    
    void Start()
    {
       tempo =  GameObject.Find("ControlGame").GetComponent<game_controller>().consultarTemp();
       punt = GameObject.Find("Score").GetComponent<ScoreController>();
        
    }

    [System.Obsolete]
    IEnumerator animacion()
    {
        Animator effect = gameObject.GetComponent<Animator>();
        
        effect.Play("Efecro");
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("ControlGame").GetComponent<game_controller>().restarNotas();
        punt.acertado();
        DestroyObject(gameObject);

    }

    [System.Obsolete]
    void Update()
    {
        
        if(gameObject.transform.position.y < -400)
        {
            GameObject.Find("ControlGame").GetComponent<game_controller>().restarNotas();
            punt.fallo();
            DestroyObject(gameObject);
        }
        if (GameObject.Find("ControlGame").GetComponent<game_controller>().estadoJuego() != true)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - this.tempo * Time.deltaTime, gameObject.transform.position.z);

        }
    }
  
    public void colision()
    {
        StartCoroutine("animacion");
        
    }

    
}
