using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class UpdateNetworkManagerAddress : MonoBehaviour
{
    public UnityTransport unityTransport = null;
    public GameObject ipField = null, portField = null;
    private TMP_InputField ipInputField = null, portInputField = null;

    private void Start()
    {
        if (ipField == null || portField == null) return;
        if (unityTransport == null) return;

        ipInputField = ipField.GetComponent<TMP_InputField>();
        portInputField = portField.GetComponent<TMP_InputField>();
    }

    public void updateIp()
    {
        if (ipInputField == null) return;

        string inputText = ipInputField.text;
        if (!validateIp(inputText)) return;
        unityTransport.ConnectionData.Address = inputText;
    }

    public void updatePort()
    {
        if (portInputField == null) return;

        string inputText = portInputField.text;
        if (!validatePort(inputText)) return;
        unityTransport.ConnectionData.Port = Convert.ToUInt16(inputText);
    }

    private bool validateIp(string ip)
    {
        return true;
    }

    private bool validatePort(string port)
    {
        return true;
    }

}
