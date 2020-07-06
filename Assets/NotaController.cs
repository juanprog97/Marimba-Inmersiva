using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaController : MonoBehaviour
{
    // Start is called before the first frame update
    private int tempo;
    void Start()
    {
       tempo =  GameObject.Find("ControlGame").GetComponent<game_controller>().consultarTemp();
    }

    [System.Obsolete]
    IEnumerator animacion()
    {
        Animator effect = gameObject.GetComponent<Animator>();
        effect.Play("Efecro");
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("ControlGame").GetComponent<game_controller>().restarNotas();
        DestroyObject(gameObject);

    }

    [System.Obsolete]
    void Update()
    {
        if(GameObject.Find("ControlGame").GetComponent<game_controller>().estadoJuego()!= true )
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - this.tempo* Time.deltaTime  , gameObject.transform.position.z);

        }
        if(gameObject.transform.position.y < -400)
        {
            GameObject.Find("ControlGame").GetComponent<game_controller>().restarNotas();
            DestroyObject(gameObject);
        }
    }
  
    public void colision()
    {
        StartCoroutine("animacion");
        
    }

    
}
