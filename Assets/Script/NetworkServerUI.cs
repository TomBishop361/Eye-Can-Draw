
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


    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        
    }

    public void Join()
    {
        NetworkManager.Singleton.StartClient();
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
        //Checks to see if connected client is not host
        if (clientId != NetworkManager.Singleton.LocalClientId)
        {
            Manager.Instance.DeviceConnected();
        }
    }

}
