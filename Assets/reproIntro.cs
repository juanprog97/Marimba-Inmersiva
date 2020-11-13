using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reproIntro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("wait");
    }

    // Update is called once per frame
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene("ConfigurationBluetooth");
    }
}
