using UnityEngine;
using System;
using System.Collections;

public class componentBluetooth : MonoBehaviour
{
    private string commandt;
    public static componentBluetooth Instance { get; private set; }
    public bool IsConnected;
    public string dataRecived = "";
    public event EventHandler seDesconecto;
    public event EventHandler seTocoBoton;

    private float time = 0.0f;
    public float interpolationPeriod = 0.025f;


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
        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            time = 0.0f;

            if (IsConnected)
            {
                try
                {
                    string datain = BluetoothService.ReadFromBluetooth();
                    if (datain.Length > 1)
                    {
                        dataRecived = datain;
                        if (dataRecived.Length == 12)
                        {
                            int cont = 0;
                            int bandera = 0;
                            while (bandera == 0)
                            {
                                if (dataRecived[cont] == '1')
                                {
                                    seTocoBoton?.Invoke(this, EventArgs.Empty);
                                    bandera = 1;
                                }
                                cont += 1;
                                if (cont == 12)
                                {
                                    bandera = 1;
                                }
                            }
                        }

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