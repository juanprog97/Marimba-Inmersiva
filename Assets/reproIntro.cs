using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class reproIntro : MonoBehaviour
{
    public GameObject videoP;
    void Awake()
    {
        videoP.GetComponent<VideoPlayer>().loopPointReached += finalizoVideo;
    }

    private void finalizoVideo(VideoPlayer source)
    {
        videoP.GetComponent<VideoPlayer>().Stop();
        SceneManager.LoadScene("ConfigurationBluetooth");
    }

}
