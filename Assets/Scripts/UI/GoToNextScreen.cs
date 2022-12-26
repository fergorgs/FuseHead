using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GoToNextScreen : MonoBehaviour
{
    private ChangeScreenButton changeScreenController;
    public GameObject nextScreenIfHost, nextScreenIfClient;
    public GameObject nextScreenSelectedObjectIfHost, nextScreenSelectedObjectIfClient;
    public bool startHostOnChange = true, startClientOnChange = true;
    public GameObject multiplayerManager = null;
    private HostInstance hostInstance = null;
    private bool connectionSucceded = false;

    private void Start()
    {
        changeScreenController = GetComponent<ChangeScreenButton>();
        hostInstance = multiplayerManager.GetComponent<HostInstance>();
    }

    public void NextScreen()
    {
        if (hostInstance.isHostInstance)
        {
            changeScreenController.nextScreen = nextScreenIfHost;
            changeScreenController.nextScreenSelectedObject = nextScreenSelectedObjectIfHost;
            if(startHostOnChange)
            {
                connectionSucceded = NetworkManager.Singleton.StartHost();
                if (connectionSucceded) changeScreenController.ChangeScreen();
            }
            else changeScreenController.ChangeScreen();
        }
        else
        {
            changeScreenController.nextScreen = nextScreenIfClient;
            changeScreenController.nextScreenSelectedObject = nextScreenSelectedObjectIfClient;
            if (startClientOnChange)
            {
                connectionSucceded = NetworkManager.Singleton.StartClient();
                if (connectionSucceded) changeScreenController.ChangeScreen();
            }
            else changeScreenController.ChangeScreen();
        }
    }
}