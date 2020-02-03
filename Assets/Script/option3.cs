using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class option3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject indexView;
    public GameObject component;
    // Update is called once per frame
    void Update()
    {
        if (indexView.GetComponent<MenuController>().getIndex() == 2)
        {
            Debug.Log("opcion3");
        }

    }
}
