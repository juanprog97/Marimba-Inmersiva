using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Vuforia;

public class vuforiaScript : MonoBehaviour, ITrackableEventHandler
{

    private string commandt;
    private TrackableBehaviour mTrackableBehaviour;
    public GameObject notFound;
    private int Undo;
    public GameObject Menu;
    public GameObject pruebIndex;
    [Obsolete]

    void Start()
    {
        Undo = 0;
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

    }

            // Update is called once per frame
    void Update()
    {
        commandt = componentBluetooth.Instance.dataRecived;
      
        
        if ((commandt[0] == '1' || commandt[1] == '1' || commandt[2] == '1') && Undo!= 1)
        {

            Undo = 1;
            if (commandt[2] == '1')
            {
            int opcion = Menu.GetComponent<MenuController>().getIndex();
                if (opcion  == 0)
                {
                    pruebIndex.GetComponent<TextMeshPro>().text = opcion.ToString();
                }
                else if(opcion == 1)
                {
                    pruebIndex.GetComponent<TextMeshPro>().text = opcion.ToString();
                }
                else
                {
                    pruebIndex.GetComponent<TextMeshPro>().text = opcion.ToString();
                }
            }
            else
            {
                    Menu.GetComponent<MenuController>().moveMenu(commandt);

            }
        }
        else if(commandt[0] == '0' || commandt[1] == '0' || commandt[2] == '0')
        {
           Undo = 0;
        }
        
    }


    public void OnTrackableStateChanged(
     TrackableBehaviour.Status previousStatus,
     TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost();
        }
        else
        {

            OnTrackingLost();
        }
    }

    void OnTrackingFound()
    {

        Menu.SetActive(true);
        notFound.SetActive(false);


    }
    void OnTrackingLost()
    {
        Menu.SetActive(false);
        notFound.SetActive(true);
    }



}