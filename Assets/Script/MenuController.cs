using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private int index = 0;
    public GameObject gameObjectAnimations1;
    public GameObject gameObjectAnimations2;
    public GameObject gameObjectAnimations3;
    void Start()
    {
        Animator anim1;
        Animator anim2;
        Animator anim3;
        anim1 = gameObjectAnimations1.GetComponent<Animator>();
        anim2 = gameObjectAnimations2.GetComponent<Animator>();
        anim3 = gameObjectAnimations3.GetComponent<Animator>();
        NewMethod(anim1, anim2, anim3);

    }

    private static void NewMethod(Animator anim1, Animator anim2, Animator anim3)
    {
        anim1.Play("selected");
        anim2.Play("Deselect2");
        anim3.Play("Deselected3");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index++;
            if (index > 2)
            {
                index = 0;
            }
            Debug.Log(index);
        }

    }
    public int getIndex()
    {
        return this.index;
    }

    // Update is called once per frame
}
