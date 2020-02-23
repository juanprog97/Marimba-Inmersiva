using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechTweaking.Bluetooth;
using UnityEngine.UI;

public class BluetoothController : MonoBehaviour
{
    public GameObject state;
    public Sprite check;
    public Sprite wrong;
    private Image change;
    public GameObject setText;
    public GameObject animationSearch;
    private BluetoothDevice device;
    void Awake()
    {
        device = new BluetoothDevice();

        if (BluetoothAdapter.isBluetoothEnabled())
        {

            connect();
        }
        else
        {

            //BluetoothAdapter.enableBluetooth(); //you can by this force enabling Bluetooth without asking the user

            BluetoothAdapter.OnBluetoothStateChanged += HandleOnBluetoothStateChanged;
            BluetoothAdapter.listenToBluetoothState(); // if you want to listen to the following two events  OnBluetoothOFF or OnBluetoothON

            BluetoothAdapter.askEnableBluetooth();//Ask user to enable Bluetooth

        }
    }

    private IEnumerator wait()
    {
        //show animate out animation

        yield return new WaitForSeconds(2f);
        //load the scene we want
    }
    void Start()
    {
        wait();
        BluetoothAdapter.OnDeviceDiscovered += BluetoothAdapter_OnDeviceDiscovered;
        BluetoothAdapter.OnDeviceOFF += HandleOnDeviceOff;//This would mean a failure in connection! the reason might be that your remote device is OFF

        BluetoothAdapter.OnDeviceNotFound += HandleOnDeviceNotFound; //Because connecting using the 'Name' property is 

    }

    private void BluetoothAdapter_OnDeviceDiscovered(BluetoothDevice dev, short s)
    {
        if (!string.IsNullOrEmpty(dev.Name))
        {
            this.animationSearch.SetActive(false);
            this.state.SetActive(true);
            this.change = this.state.GetComponent<Image>();
            this.change.sprite = this.check;

        }
    }



    // Update is called once per frame
    private void connect()
    {





        /* The Property device.MacAdress doesn't require pairing. 
		 * Also Mac Adress in this library is Case sensitive,  all chars must be capital letters
		 */
        //device.MacAddress = "XX:XX:XX:XX:XX:XX";


        device.Name = "CasaMemoriaTumaco";
        /*
        * * 
		* Trying to identefy a device by its name using the Property device.Name require the remote device to be paired
		* but you can try to alter the parameter 'allowDiscovery' of the Connect(int attempts, int time, bool allowDiscovery) method.
		* allowDiscovery will try to locate the unpaired device, but this is a heavy and undesirable feature, and connection will take a longer time
		*/


        /*
		 * 10 equals the char '\n' which is a "new Line" in Ascci representation, 
		 * so the read() method will retun a packet that was ended by the byte 10. simply read() will read lines.
		 * If you don't use the setEndByte() method, device.read() will return any available data (line or not), then you can order them as you want.
		 */
        device.setEndByte(10);


        /*
		 * The ManageConnection Coroutine will start when the device is ready for reading.
		 */


        device.connect();


    }

    


    void HandleOnBluetoothStateChanged(bool isBtEnabled)
    {
        if (isBtEnabled)
        {
            connect();
            //We now don't need our recievers
            BluetoothAdapter.OnBluetoothStateChanged -= HandleOnBluetoothStateChanged;
            BluetoothAdapter.stopListenToBluetoothState();
        }
    }



    void HandleOnDeviceOff(BluetoothDevice dev)
    {
        if (!string.IsNullOrEmpty(dev.Name))
        {
            this.animationSearch.SetActive(false);
            this.state.SetActive(true);
            this.change = this.state.GetComponent<Image>();
            this.change.sprite = this.wrong;
        }
     
    }

    //Because connecting using the 'Name' property is just searching, the Plugin might not find it!.
    void HandleOnDeviceNotFound(BluetoothDevice dev)
    {
        if (!string.IsNullOrEmpty(dev.Name))
        {
            this.animationSearch.SetActive(false);
            this.state.SetActive(true);
            this.change = this.state.GetComponent<Image>();
            this.change.sprite = this.wrong;
        }
    }

    public void disconnect()
    {
        if (device != null)
            device.close();
    }

}
