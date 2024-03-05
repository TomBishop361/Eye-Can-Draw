using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetBehaviour : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI promptText;

    [Rpc(SendTo.NotOwner)]
    public void promptChangeClientRpc(string prompt)
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            Debug.Log(prompt);
            promptText.text = prompt;
        }
    }

}
