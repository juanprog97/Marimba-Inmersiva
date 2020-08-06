
using UnityEngine;

public class note_s : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    void OnTriggerEnter(Collider collision )
    {
        parent.GetComponent<NotaController>().colision();
    }

 

 


}
