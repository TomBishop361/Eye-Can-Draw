
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkServerUI : MonoBehaviour
{
    [SerializeField] private Button connect; //for phone

    [SerializeField] ClientScript ClientScript;

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        if (connect != null) connect.onClick.AddListener(() => { NetworkManager.Singleton.StartClient();});
    }

    private void OnGUI()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld) { 
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
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
