using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyC : MonoBehaviour
{
   
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Driver");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
