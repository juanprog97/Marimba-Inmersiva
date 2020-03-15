using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class option1 : MonoBehaviour
{

    public GameObject indexView;
    public GameObject component;
    private Animator anim;
    private int sta = 0;
    private int key = 0;
    void start()
    {
        this.anim = component.GetComponent<Animator>();
        this.anim.Play("Selected");
    }
    void Update()
    {
        this.anim = component.GetComponent<Animator>();
        if (indexView.GetComponent<MenuController>().getEnterValue() && indexView.GetComponent<MenuController>().getIndex() == 0)
        {
            StartCoroutine(LoadSceneAFterTransition(this.anim));
            this.key = 1;
        }

        else if (indexView.GetComponent<MenuController>().getIndex() == 0 && this.key !=1)
        {
            this.anim.Play("Selected");
            this.sta = 1;
        }
        else if ((indexView.GetComponent<MenuController>().getIndex() != 0 || this.sta == 1) && this.key != 1)
        {
            this.anim.Play("Deselect");
            this.sta = 0;
        }

        


    }
    private IEnumerator LoadSceneAFterTransition(Animator an)
    {
        //show animate out animation
        an.Play("Onpress");
        yield return new WaitForSeconds(2f);
        //load the scene we want
        //SceneManager.LoadScene(1);
    }
}
