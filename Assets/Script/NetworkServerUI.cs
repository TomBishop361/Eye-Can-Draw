
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
    }

    //Starts server as host
    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        
    }

    //Connects to server as client
    public void Join()
    {
        NetworkManager.Singleton.StartClient();
    }

    //When client connects
    private void OnClientConnected(ulong clientId)
    {
        //if connecting is Not Server
        if (!NetworkManager.Singleton.IsServer)
        {
            //Connecting Client
            if (clientId == NetworkManager.Singleton.LocalClientId)
            {
                Debug.Log("Client connected. You can now call ServerRpc methods.");
                ClientScript.setupClient();
                
            }
        }
        //Host
        if (clientId != NetworkManager.Singleton.LocalClientId)
        {
            Manager.Instance.DeviceConnected();
        }
    }

}
