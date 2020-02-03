using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class option2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject indexView;
    public GameObject component;

    void Update()
    {
        if (indexView.GetComponent<MenuController>().getIndex() == 1)
        {
            Debug.Log("opcion2");
        }

    }
}
