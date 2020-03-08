using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vuforiaScript : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public GameObject xPos;
    public GameObject yPos;
    public GameObject Menu;
    public GameObject zPos;
    public GameObject notFound;
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
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
    private void OnTrackingFound()
    {
        Menu.SetActive(true);
        notFound.SetActive(false);

        xPos.GetComponent<UnityEngine.UI.Text>().text = mTrackableBehaviour.transform.position.x.ToString();
        yPos.GetComponent<UnityEngine.UI.Text>().text = mTrackableBehaviour.transform.position.y.ToString();
        zPos.GetComponent<UnityEngine.UI.Text>().text = mTrackableBehaviour.transform.position.z.ToString();
        
        
    }
    private void OnTrackingLost()
    {
        Menu.SetActive(false);
        notFound.SetActive(true);
    }
}


