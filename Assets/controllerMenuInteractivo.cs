using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controllerMenuInteractivo : MonoBehaviour
{
    public GameObject tutorialGafa;
    public GameObject tutorialModulo;
    public void cargarModuloHistoria()
    {
        tutorialGafa.GetComponent<controllerGafas>().setOpcionModulo(1);
        tutorialModulo.SetActive(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void cargarModuloCultura()
    {
        tutorialGafa.GetComponent<controllerGafas>().setOpcionModulo(2);
        tutorialModulo.SetActive(true);
        gameObject.SetActive(false);
    }

    public void devolver()
    {
        SceneManager.LoadScene("PrincipalMenu");
    }
}
