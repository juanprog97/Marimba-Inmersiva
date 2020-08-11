using UnityEngine;
using System;

public class componentBluetooth : MonoBehaviour
{
    private string commandt;
    public static componentBluetooth Instance { get; private set; }
    public bool IsConnected;
    public string dataRecived = "";
    public event EventHandler seDesconecto;
    
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            BluetoothService.CreateBluetoothObject();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        IsConnected = false;
       
        if (!IsConnected)
        {
            IsConnected = BluetoothService.StartBluetoothConnection("CasaMemoriaTumaco");
        }
    }

    public bool estaConectado()
    {
        return this.IsConnected;
    }

    public bool reconnect()
    {
       
        if (!IsConnected)
        {
            IsConnected = BluetoothService.StartBluetoothConnection("CasaMemoriaTumaco");
            

        }
        return IsConnected;


    }

    void Update()
    {
        if (IsConnected)
        {
            try
            {
                string datain = BluetoothService.ReadFromBluetooth();
                if (datain.Length > 1)
                {
                    dataRecived = datain;
                }

            }
            catch (Exception e)
            {
                dataRecived = "000000000000";
                IsConnected = false;
                seDesconecto?.Invoke(this, EventArgs.Empty);
            }
        }

    }
    public void disconnect()
    {
        /* if (device != null)
             device.close();*/
        if (IsConnected)
        {
            BluetoothService.StopBluetoothConnection();
        }
    }

}