using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine.Networking;
using UnityEditor.PackageManager;
using System.Xml.Serialization;

public class NetworkServerUI : MonoBehaviour
{

    [SerializeField]ClientScript ClientScript;

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnGUI()
    {
        
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Host"))
            {
                NetworkManager.Singleton.StartHost();
                Debug.Log(NetworkManager.Singleton.IsServer);
                Debug.Log(NetworkManager.Singleton.IsHost);
                Debug.Log(NetworkManager.Singleton.IsClient);


            }
            if (GUILayout.Button("Client"))
            {
                NetworkManager.Singleton.StartClient();
                Debug.Log(NetworkManager.Singleton.IsServer);
                Debug.Log(NetworkManager.Singleton.IsHost);
                Debug.Log(NetworkManager.Singleton.IsClient);
            }
        }
        GUILayout.EndArea();
    }

    

    private void OnClientConnected(ulong clientId)
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            if (clientId == NetworkManager.Singleton.LocalClientId)
            {
                Debug.Log("Client connected. You can now call ServerRpc methods.");
                ClientScript.setupClient();
            }
        }
    }

}
