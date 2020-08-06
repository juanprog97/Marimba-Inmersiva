using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class option2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject indexView;
    public GameObject component;
    private Animator anim;
    private int sta = 0;
    private int key = 0;

    void Update()
    {
        this.anim = component.GetComponent<Animator>();
        if (indexView.GetComponent<MenuController>().getEnterValue() && indexView.GetComponent<MenuController>().getIndex() == 1)
        {
            this.anim.Play("onPress2");
            this.key = 1;
        }
        if (indexView.GetComponent<MenuController>().getIndex() == 1 && this.key != 1)
        {
            this.anim.Play("Selectd2");
            this.sta = 1;
        }
        else if ((indexView.GetComponent<MenuController>().getIndex() != 1 || this.sta == 1) && this.key != 1)
        {
            this.anim.Play("Deselect2");
            this.sta = 0;
        }
      

    }
    private IEnumerator LoadSceneAFterTransition(Animator an)
    {
        //show animate out animation
        an.Play("onPress2");
        yield return new WaitForSeconds(2f);
        //load the scene we want
       // SceneManager.LoadScene(0);
    }
}
