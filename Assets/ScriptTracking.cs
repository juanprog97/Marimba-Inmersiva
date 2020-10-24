using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ScriptTracking : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public GameObject GameApp;
    public GameObject NotFound;
    public bool estado;


    void Start()
    {
        // Query Vuforia for recommended frame rate and set it in Unity
        int targetFps = VuforiaRenderer.Instance.GetRecommendedFps(VuforiaRenderer.FpsHint.NONE);

        // By default, we use Application.targetFrameRate to set the recommended frame rate.
        // If developers use vsync in their quality settings, they should also set their
        // QualitySettings.vSyncCount according to the value returned above.
        // e.g: If targetFPS > 50 --> vSyncCount = 1; else vSyncCount = 2;
        if (Application.targetFrameRate != targetFps)
        {

            Application.targetFrameRate = targetFps;
        }
        estado = false;
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    // Update is called once per frame
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            
            OnTrackingLost();
        }
    }

    public bool getEstado()
    {
        return this.estado;
    }

    protected virtual void OnTrackingFound()
    {
        NotFound.SetActive(false);
        GameApp.SetActive(true);
        this.estado = true;

    }


    protected virtual void OnTrackingLost()
    {
        GameApp.SetActive(false);
        this.estado = false;
        NotFound.SetActive(true);

    }
}
