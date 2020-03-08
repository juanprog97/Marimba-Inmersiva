using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechTweaking.Bluetooth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BluetoothController : MonoBehaviour
{
    public GameObject state;
    public Sprite check;
    public Sprite wrong;
    public GameObject setText;
    public GameObject animationSearch;
    public GameObject reload;
    private static BluetoothDevice device;
    void Awake()
    {


        this.animationSearch.SetActive(true);
        this.setText.GetComponent<TMPro.TextMeshProUGUI>().text = "Buscando Sincronizacion...";
        BluetoothAdapter.enableBluetooth(); //you can by this force enabling Bluetooth without asking the user
        device = new BluetoothDevice();
        device.Name = "CasaMemoriaTumaco";
        device.setEndByte(10);
        device.ReadingCoroutine = ManageConnection;

        device.connect();        //StartCoroutine(wait(3));

    }
    void Start()
    {
        UnityEngine.XR.XRSettings.enabled = false;
        //this.setText.GetComponent<TMPro.TextMeshProUGUI>().text = "Buscando Sincronizacion...";

        BluetoothAdapter.OnConnected += HandleOnConnected;
        BluetoothAdapter.OnDeviceNotFound += HandleOnDeviceNotFound;
        BluetoothAdapter.OnDeviceOFF += HandleOnDeviceOff;

        //BluetoothAdapter.OnDeviceOFF += HandleOnDeviceOff;//This would mean a failure in connection! the reason might be that your remote device is OFF

        //BluetoothAdapter.OnDeviceNotFound += HandleOnDeviceNotFound; //Because connecting using the 'Name' property is 

    }
    void HandleOnDeviceOff(BluetoothDevice dev)
    {
        if (!string.IsNullOrEmpty(dev.Name))
        {
            StartCoroutine(wait(4));

        }
    }
    void HandleOnDeviceNotFound(BluetoothDevice dev)
    {
        if (!string.IsNullOrEmpty(dev.Name))
        {
            StartCoroutine(wait(4));

        }
    }


    void HandleOnConnected(BluetoothDevice dev)
    {
        if (!string.IsNullOrEmpty(dev.Name))
        {

            StartCoroutine(wait(2));

        }
    }


    IEnumerator ManageConnection(BluetoothDevice device)
    {
      
        while (device.IsReading)
        {
            if (device.IsDataAvailable)
            {
                byte[] msg = device.read();//because we called setEndByte(10)..read will always return a packet excluding the last byte 10.
               
                if (msg != null && msg.Length > 0)
                {
                    string content = System.Text.ASCIIEncoding.ASCII.GetString(msg);
                    
                }

            }

            yield return null;
        }
        
        

    }


    private IEnumerator wait(int option)
    {
        //show animate out animation
        if(option == 1)
        {
            this.reload.GetComponent<Animator>().Play("Pressed");
            yield return new WaitForSeconds(2);
            this.reload.SetActive(false);
            this.disconnect();
            this.Awake();
            


        }

        if(option == 2)
        {
            this.animationSearch.SetActive(false);
            this.state.SetActive(true);
            this.state.GetComponent<Image>().sprite = this.check;
            this.setText.GetComponent<TMPro.TextMeshProUGUI>().text = "Sincronizacion realizada correctamente";
            yield return new WaitForSeconds(4);
            this.wait(3);
            
        }
        if(option == 3)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);

        }

    
        if(option == 4)
        {
            this.animationSearch.SetActive(false);
            this.state.SetActive(true);
            this.state.GetComponent<Image>().sprite = this.wrong;
            this.setText.GetComponent<TMPro.TextMeshProUGUI>().text = "No se pudo sincronizar la conexion.";
            yield return new WaitForSeconds(4);
            this.setText.GetComponent<TMPro.TextMeshProUGUI>().text = "Vuelva intentar la sincronizacion";
            this.state.SetActive(false);
            this.reload.SetActive(true);
            

        }

          
        //load the scene we want
    }

   

   



    public void reset()
    {
        
        StartCoroutine(this.wait(1));
        


    }

    

    






 

    //Because connecting using the 'Name' property is just searching, the Plugin might not find it!.

    public void disconnect()
    {
        if (device != null)
            device.close();
    }

}
