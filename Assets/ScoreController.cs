using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    private int puntaje;
    private int numAcertadas;
    private int pxt;
    private TextMeshPro scoreInt;
    private TextMeshPro multiplicador;
    

    [System.Obsolete]
    void Start()
    {
        this.scoreInt = gameObject.transform.FindChild("nPuntaje").GetComponent<TextMeshPro>();
        this.multiplicador = gameObject.transform.FindChild("multiplicador").GetComponent<TextMeshPro>();
        this.scoreInt.text = "0";
        this.multiplicador.text = "x1";
        this.puntaje = 0;
        this.numAcertadas = 0;
        this.pxt = 1;

    }


    public void acertado()
    {
        this.numAcertadas += 1;
   
        //Debug.Log(this.numAcertadas.ToString());
        if (this.numAcertadas > 24)
        {
            this.pxt = 5;
        }
        else if(this.numAcertadas < 24 && this.numAcertadas >18 )
        {
            this.pxt = 4;
        }
        else if (this.numAcertadas < 18 && this.numAcertadas > 12)
        {
            this.pxt = 3;
        }
        else if (this.numAcertadas < 12 && this.numAcertadas > 6)
        {
            this.pxt = 2;
        }
        else if(numAcertadas<6)
        {
            this.pxt = 1;
        }
        this.puntaje += 100 * this.pxt;
        this.scoreInt.text = this.puntaje.ToString();
        this.multiplicador.text = "x" + pxt.ToString();

    }
    public void fallo()
    {
       
   
        if (this.numAcertadas > 24)
        {
            this.numAcertadas = 19;
        }
        else if (this.numAcertadas < 24 && this.numAcertadas > 18)
        {
            this.numAcertadas = 12;
        }
        else if (this.numAcertadas < 18 && this.numAcertadas > 12)
        {
            this.numAcertadas = 6;
        }
        else if (this.numAcertadas < 12 && this.numAcertadas > 6)
        {
            this.numAcertadas = 0;
        }

        this.multiplicador.text = "x" + this.pxt.ToString();
       

    }

    public int getPuntaje()
    {
        return this.puntaje;
    }
    public void reset()
    {
        this.puntaje = 0;
        this.numAcertadas = 0;
        this.scoreInt.text = this.puntaje.ToString();
        this.multiplicador.text = "x" + pxt.ToString();
    }




    // Update is called once per frame
 
}
