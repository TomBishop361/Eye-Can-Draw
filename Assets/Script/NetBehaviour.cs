using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetBehaviour : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI promptText;

    //sends to all clients
    [Rpc(SendTo.NotOwner)]
    public void promptChangeClientRpc(string prompt)
    {
        //if client is NOT also server (Host), display prompt text
        if (!NetworkManager.Singleton.IsServer)
        {
            promptText.text = prompt;
        }
    }

}
