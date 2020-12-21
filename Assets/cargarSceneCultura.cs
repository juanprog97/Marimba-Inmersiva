using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class cargarSceneCultura : MonoBehaviour
{

    private string UrlFirebase = "gs://quickstart-1595792293378.appspot.com/AssetsBundles/MaterialMultimedia/cultura/";
    public GameObject pantallaDescarga;
    public GameObject pantallaElementos;
    public Animation first;
    public Animation second;
    public GameObject itemTexto;
    List<RenderTexture> renderTexture;
    public GameObject audioElement;
    public GameObject titleScene;
    public GameObject videoElement;
    public GameObject imageElement;
    private UI_codeMultimedia.Escena EscenaSeleccionada;
    private string AssetName;
    List<RenderTexture> renders;

    public GameObject PantallaMenu;
    public GameObject CodeUI;
    private List<GameObject> ObjetosEscenas;

    public GameObject instruccionesEscena;


    public void setEscenaSeleccionada(UI_codeMultimedia.Escena escena)
    {
        this.EscenaSeleccionada = escena;
        this.AssetName = escena.AssetName;
    }



    [Obsolete]
    void distribuirElementos(AssetBundle asset)
    {
        GameObject elemTmp;
        first.Stop();
        second.Stop();
        renders = new List<RenderTexture>();
        pantallaDescarga.SetActive(false);
        pantallaElementos.SetActive(true);
        titleScene.GetComponent<Text>().text = EscenaSeleccionada.Title;
        for (int i = 0; i < EscenaSeleccionada.Materials.Count; i++)
        {
            string elem = EscenaSeleccionada.Materials[i].Type;
            UI_codeMultimedia.Material material = EscenaSeleccionada.Materials[i];
            if (elem == "image")
            {
                elemTmp = Instantiate(imageElement, pantallaElementos.transform.GetComponent<Transform>());
                elemTmp.GetComponent<Image>().sprite = asset.LoadAsset<Sprite>(material.Name);
                //Posicion y Escala
                elemTmp.transform.localScale = new Vector3(1, 1, 1);
                elemTmp.GetComponent<RectTransform>().sizeDelta = new Vector2(material.Propierty.Dimension.Width,
                    material.Propierty.Dimension.Height);
                elemTmp.GetComponent<RectTransform>().localPosition = new Vector3(material.Propierty.Position.PosX,
                    material.Propierty.Position.PosY, material.Propierty.Position.PosZ);

                
                ObjetosEscenas.Add(elemTmp);

            }
            else if (elem == "3d")
            {

               
                elemTmp = Instantiate(asset.LoadAsset<GameObject>(material.Name),
                   pantallaElementos.transform.GetComponent<Transform>());
                //Posicion y Escala
                 elemTmp.transform.localScale = new Vector3(material.Propierty.Scale.ScaleX, material.Propierty.Scale.ScaleY,
                     material.Propierty.Scale.ScaleZ);
                elemTmp.transform.localPosition = new Vector3(material.Propierty.Position.PosX,
                    material.Propierty.Position.PosY, material.Propierty.Position.PosZ);
                LeanTween.rotateAroundLocal(elemTmp, Vector3.forward, 360f, 6f).setRepeat(-1);
                ObjetosEscenas.Add(elemTmp);
            }
            else if (elem == "audio")
            {

                audioElement.transform.FindChild("sample").GetComponent<AudioSource>().clip =
                    asset.LoadAsset<AudioClip>(material.Name);
                audioElement.SetActive(true);
                audioElement.transform.FindChild("Titulo").GetComponent<Text>().text = material.Description;
                audioElement.transform.FindChild("Titulo").GetComponent<RectTransform>().sizeDelta =
                    new Vector2(material.Propierty.Dimension.Width,
                   material.Propierty.Dimension.Height);
                audioElement.transform.FindChild("Titulo").GetComponent<RectTransform>().localPosition =
                    new Vector3(material.Propierty.Position.PosX,
                    material.Propierty.Position.PosY, material.Propierty.Position.PosZ);
                audioElement.transform.FindChild("sample").GetComponent<AudioSource>().Play();
            }
            else if (elem == "text")
            {

                elemTmp = Instantiate(itemTexto, pantallaElementos.transform.GetComponent<Transform>());
                elemTmp.GetComponent<RectTransform>().sizeDelta = new Vector2(material.Propierty.Dimension.Width,
                   material.Propierty.Dimension.Height);
                elemTmp.GetComponent<RectTransform>().localPosition = new Vector3(material.Propierty.Position.PosX,
                    material.Propierty.Position.PosY, material.Propierty.Position.PosZ);
                elemTmp.transform.localScale = new Vector3(1, 1, 1);
                elemTmp.GetComponent<Text>().text = material.Description;
                elemTmp.transform.name = "texto" + i.ToString();
                elemTmp.GetComponent<Text>().fontSize = Convert.ToInt32(material.Propierty.FontSize);

                ObjetosEscenas.Add(elemTmp);

            }
            else if (elem == "video")
            {

                RenderTexture texture = new RenderTexture(1280, 720, 1);
                elemTmp = Instantiate(videoElement, pantallaElementos.transform.GetComponent<Transform>());
                VideoClip video = asset.LoadAsset<VideoClip>(material.Name);
                elemTmp.transform.FindChild("videoSample").GetComponent<VideoPlayer>().clip = video;
                elemTmp.transform.FindChild("videoSample").GetComponent<VideoPlayer>().targetTexture = texture;
                elemTmp.name = "video" + i.ToString();
                elemTmp.transform.FindChild("sample").GetComponent<RectTransform>().sizeDelta = new Vector2(material.Propierty.Dimension.Width,
                   material.Propierty.Dimension.Height);
                elemTmp.transform.FindChild("sample").GetComponent<RectTransform>().localPosition = new Vector3(material.Propierty.Position.PosX,
                    material.Propierty.Position.PosY, material.Propierty.Position.PosZ);
                elemTmp.transform.FindChild("sample").GetComponent<RawImage>().texture = texture;
                elemTmp.transform.FindChild("sample").transform.localScale = new Vector3(1, 1, 1);
                elemTmp.transform.FindChild("videoSample").GetComponent<VideoPlayer>().Play();
                renders.Add(texture);
                ObjetosEscenas.Add(elemTmp);
            }

            //Desplegar Texto y titulo
        }

    }


    [System.Obsolete]
    IEnumerator DescargarPaquete()
    {
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference reference = storage.GetReferenceFromUrl(UrlFirebase + AssetName);

        var task = reference.GetDownloadUrlAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        while (!Caching.ready)
            yield return null;

        WWW www = WWW.LoadFromCacheOrDownload(task.Result.ToString(), this.EscenaSeleccionada.Version);


        while (!www.isDone)
        {
            pantallaDescarga.transform.FindChild("porce").GetComponent<Text>().text = Convert.ToInt32(www.progress * 100).ToString() + " %";
            yield return null;
        }
        pantallaDescarga.transform.FindChild("porce").GetComponent<Text>().text = 0.ToString() + " %";
        instruccionesEscena.SetActive(true);

        if (www.error == null)
        {
            Debug.Log(AssetName);
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

    [Obsolete]
    void OnDisable()
    {
        componentBluetooth.Instance.seTocoBoton -= Instance_seTocoBoton;
        foreach (GameObject obj in ObjetosEscenas)
        {
            DestroyObject(obj);
        }
        foreach (RenderTexture ren in renders)
        {
            Destroy(ren);
        }
        audioElement.SetActive(false);
        renders.Clear();
        ObjetosEscenas.Clear();
        renders = null;
        ObjetosEscenas = null;
        instruccionesEscena.SetActive(false);
    }
    void OnEnable()
    {
        componentBluetooth.Instance.seTocoBoton += Instance_seTocoBoton;
        ObjetosEscenas = new List<GameObject>();
        Caching.compressionEnabled = false;
        iniciarDescargar();

    }


    private void salir()
    {
        pantallaElementos.SetActive(false);
        PantallaMenu.SetActive(true);
        CodeUI.SetActive(true);
        gameObject.SetActive(false);
    }

     /*void OnGUI()
       {
           Event e = Event.current;
           if (e.isKey)
           {
               if (e.keyCode == KeyCode.KeypadEnter)
               {
                   salir();
               }

           }
       }*/

                private void Instance_seTocoBoton(object sender, EventArgs e)
    {
        if (componentBluetooth.Instance.dataRecived[5] == '1')
        {
    
            salir();
        }
    }

    void iniciarDescargar()
    {
        first.Play();
        second.Play();
        pantallaDescarga.SetActive(true);
        StartCoroutine("DescargarPaquete");
    }


}
   

