using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class cargarScene : MonoBehaviour
{
    private string UrlFirebase = "gs://quickstart-1595792293378.appspot.com/AssetsBundles/MaterialMultimedia/";
    public GameObject pantallaDescarga;
    public GameObject pantallaElementos;
    public Animation first;
    public Animation second;
    List<RenderTexture> renderTexture;
    public GameObject audioElement;

    private string AssetName = "prueba1";

    private List<string> listadoObjeto = new List<string> { "fotoGaitan", "Gaitan", "Cheese", "discursoGaitan", "videoExample"};
    private List<string> tipoObjeto = new List<string> { "image", "image", "3d", "audio", "video"};

    [Obsolete]
    void distribuirElementos(AssetBundle asset)
    {
        GameObject elemTmp;
        first.Stop();
        second.Stop();
        pantallaDescarga.SetActive(false);
        pantallaElementos.SetActive(true);
        renderTexture = new List<RenderTexture>();
        for (int i = 0; i< tipoObjeto.Count; i++)
        {
            string elem = tipoObjeto[i];

            if (elem == "image")
            {
                elemTmp = new GameObject(listadoObjeto[i]);
                elemTmp.AddComponent<Image>();
                elemTmp.GetComponent<Image>().sprite = asset.LoadAsset<Sprite>(listadoObjeto[i]);
                //Posicion y Escala
                elemTmp.transform.parent = pantallaElementos.transform.GetComponent<Transform>();
                elemTmp.transform.localScale = new Vector3(1, 1, 1);

            }
            else if (elem == "3d")
            {

                elemTmp = Instantiate(asset.LoadAsset<GameObject>(listadoObjeto[i]),
                    pantallaElementos.transform.GetComponent<Transform>());
                //Posicion y Escala
            }
            else if (elem == "audio")
            {
                audioElement.transform.FindChild("sample").GetComponent<AudioSource>().clip =
                    asset.LoadAsset<AudioClip>(listadoObjeto[i]);
                audioElement.transform.FindChild("sample").GetComponent<AudioSource>().Play();
            }
            else if (elem == "video")
            {
                RenderTexture renderTe = new RenderTexture(1280, 720, 1);
                renderTexture.Add(renderTe);
                elemTmp = new GameObject(listadoObjeto[i]);
                GameObject video = new GameObject("videoDireccion");
                GameObject imageTextura = new GameObject("imageRaw");
                video.AddComponent<VideoPlayer>();
                video.GetComponent<VideoPlayer>().clip = asset.LoadAsset<VideoClip>(listadoObjeto[i]);
                video.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.RenderTexture;
                video.GetComponent<VideoPlayer>().targetTexture = renderTe;
                imageTextura.AddComponent<RawImage>().texture = renderTe;

                video.transform.parent = elemTmp.GetComponent<Transform>();
                imageTextura.transform.parent = elemTmp.GetComponent<Transform>();
                //Posicion y Escala

                elemTmp.transform.parent = pantallaElementos.transform.GetComponent<Transform>();
                elemTmp.transform.localScale = new Vector3(1, 1, 1);
            }

            //Desplegar Texto y titulo
        }

    }


    [System.Obsolete]
    IEnumerator DescargarPaquete()
    {
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference reference = storage.GetReferenceFromUrl(UrlFirebase+AssetName);

        var task = reference.GetDownloadUrlAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        while (!Caching.ready)
            yield return null;

        WWW www = WWW.LoadFromCacheOrDownload(task.Result.ToString(), 2);


        while (!www.isDone)
        {
            yield return null;
        }
        
        if (www.error == null)
        {
            AssetBundle bundle = www.assetBundle;
            if (AssetName == "")
            {
                Instantiate(bundle.mainAsset);
            }
            else
            {
                distribuirElementos(bundle);
            }

            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);
        }

        else
        {
            throw new Exception("WWW download had an error:" + www.error);
        }
    }


    void Start()
    {
        iniciarDescargar();
    }

    void iniciarDescargar()
    {
        first.Play();
        second.Play();
        pantallaDescarga.SetActive(true);
        StartCoroutine("DescargarPaquete");
    }

 
}
