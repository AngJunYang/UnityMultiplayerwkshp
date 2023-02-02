using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FishNet;
using FishNet.Managing;
using FishNet.Connection;

public class NetworkUI : MonoBehaviour
{
    public NetworkManager networkManager;
    public InputField ipInputField;
    public InputField portInputField;
    public Button serverButton;
    public Button clientButton;
    public Button confirmButton;
    public Button backButton;
    public int connectType; // 1 is server, 2 is client

    public void Awake()
    {
        ipInputField.gameObject.SetActive(false);
        portInputField.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        serverButton.gameObject.SetActive(true);
        clientButton.gameObject.SetActive(true);
    }

    public void OnEnable()
    {
        InstanceFinder.ServerManager.OnServerConnectionState += DisableUI;
        InstanceFinder.ClientManager.OnClientConnectionState += DisableClientUI;
    }

    public void OnDisable()
    {
        InstanceFinder.ServerManager.OnServerConnectionState -= DisableUI;
        InstanceFinder.ClientManager.OnClientConnectionState -= DisableClientUI;
    }

    public void ShowServerConnectUI()
    {
        connectType = 1;
        serverButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
        portInputField.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void ShowClientConnectUI()
    {
        connectType = 2;
        serverButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
        ipInputField.gameObject.SetActive(true);
        portInputField.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void Connect()
    {
        if (connectType == 1)
        {
            int portInt = int.Parse(portInputField.text);
            if (portInt > 65535 || portInt < 0)
            {
                Debug.LogError("Port out of range");
                return;
            }
            InstanceFinder.TransportManager.Transport.SetPort((ushort)portInt);
            InstanceFinder.TransportManager.Transport.SetServerBindAddress(ipInputField.text, FishNet.Transporting.IPAddressType.IPv4);

            if (networkManager == null) return;
            networkManager.ServerManager.StartConnection();

            InstanceFinder.TransportManager.Transport.SetClientAddress("localhost");
            if (networkManager == null) return;
            networkManager.ClientManager.StartConnection();
        }
        else if (connectType == 2)
        {
            InstanceFinder.TransportManager.Transport.SetClientAddress(ipInputField.text);
            if (networkManager == null) return;
            networkManager.ClientManager.StartConnection();
        }       
    }

    public void BackToMainMenu()
    {
        serverButton.gameObject.SetActive(true);
        clientButton.gameObject.SetActive(true);
        ipInputField.gameObject.SetActive(false);
        portInputField.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
    }

    public void DisableUI(FishNet.Transporting.ServerConnectionStateArgs args)
    {
        ipInputField.gameObject.SetActive(false);
        portInputField.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        serverButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
    }

    public void DisableClientUI(FishNet.Transporting.ClientConnectionStateArgs args)
    {
        ipInputField.gameObject.SetActive(false);
        portInputField.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        serverButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
    }
}
