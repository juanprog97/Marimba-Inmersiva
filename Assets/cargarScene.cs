using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class cargarScene : MonoBehaviour
{
 

    private string UrlFirebase = "gs://quickstart-1595792293378.appspot.com/AssetsBundles/MaterialMultimedia/historia/";
    public GameObject pantallaDescarga;
    public GameObject pantallaElementos;
    public Animation first;
    public Animation second;
    public GameObject itemTexto;
    List<RenderTexture> renderTexture;
    public GameObject audioElement;
    public GameObject titleScene;
    public GameObject videoElement;
    private code_ui_history.Escena EscenaSeleccionada;
    private string AssetName;

   


    public void setEscenaSeleccionada(code_ui_history.Escena escena)
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
        pantallaDescarga.SetActive(false);
        pantallaElementos.SetActive(true);
        titleScene.GetComponent<Text>().text = EscenaSeleccionada.Title;
        for (int i = 0; i< EscenaSeleccionada.Materials.Count; i++)
        {
            string elem = EscenaSeleccionada.Materials[i].Type;
            code_ui_history.Material material = EscenaSeleccionada.Materials[i];
            Debug.Log(elem);
            if (elem == "image")
            {
                elemTmp = new GameObject(material.Name);
                elemTmp.AddComponent<Image>();
                elemTmp.GetComponent<Image>().sprite = asset.LoadAsset<Sprite>(material.Name);
                //Posicion y Escala
                elemTmp.transform.parent = pantallaElementos.transform.GetComponent<Transform>();
                elemTmp.transform.localScale = new Vector3(1, 1, 1);
                elemTmp.transform.rotation.Set(0, 0, -90, 0);
                elemTmp.GetComponent<RectTransform>().sizeDelta = new Vector2(material.Propierty.Dimension.Width,
                    material.Propierty.Dimension.Height);
                elemTmp.GetComponent<RectTransform>().localPosition = new Vector3(material.Propierty.Position.PosX,
                    material.Propierty.Position.PosY, material.Propierty.Position.PosZ);
               
            }
            else if (elem == "3d")
            {

                elemTmp = Instantiate(asset.LoadAsset<GameObject>(material.Name),
                    pantallaElementos.transform.GetComponent<Transform>());
                //Posicion y Escala
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
            else if(elem == "text")
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



            }
            else if (elem == "video")
            {
                elemTmp = Instantiate(videoElement, pantallaElementos.transform.GetComponent<Transform>());
                elemTmp.transform.FindChild("videoSample").GetComponent<VideoPlayer>().clip  = 
                    asset.LoadAsset<VideoClip>(material.Name);
                elemTmp.name = "video" + i.ToString();
                elemTmp.transform.FindChild("sample").GetComponent<RectTransform>().sizeDelta = new Vector2(material.Propierty.Dimension.Width,
                   material.Propierty.Dimension.Height);
                elemTmp.transform.FindChild("sample").GetComponent<RectTransform>().localPosition = new Vector3(material.Propierty.Position.PosX,
                    material.Propierty.Position.PosY, material.Propierty.Position.PosZ);
                elemTmp.transform.FindChild("sample").transform.localScale = new Vector3(1, 1, 1);
                elemTmp.transform.FindChild("videoSample").GetComponent<VideoPlayer>().Play();
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

        WWW www = WWW.LoadFromCacheOrDownload(task.Result.ToString(), this.EscenaSeleccionada.Version);


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

    void OnDisable()
    {
        Debug.Log("borrarElementos");
    }
    void OnEnable()
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
