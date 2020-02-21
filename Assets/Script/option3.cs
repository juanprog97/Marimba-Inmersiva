using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class option3 : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && indexView.GetComponent<MenuController>().getIndex() == 2)
        {
            StartCoroutine(LoadSceneAFterTransition(this.anim));
            this.key = 1;
        }
        if (indexView.GetComponent<MenuController>().getIndex() == 2 && this.key != 1)
        {
            this.anim.Play("Selected3");
            this.sta = 1;
        }
        else if ((indexView.GetComponent<MenuController>().getIndex() != 2 || this.sta == 1 ) && this.key != 1)
        {
            this.anim.Play("Deselected3");
            this.sta = 0;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {

        }

    }
    private IEnumerator LoadSceneAFterTransition(Animator an)
    {
        //show animate out animation
        an.Play("onPress3");
        yield return new WaitForSeconds(2f);
        //load the scene we want
        Application.Quit();
    }
}
