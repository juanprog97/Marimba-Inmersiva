using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        Color fond = new Color(0.3f, 0.4f, 0.6f);
        t.text = "Prueba";
        t.fontSize = 120;
        t.color = fond;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
