using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ClientScript : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject phoneCanvas;
    [SerializeField] TextMeshProUGUI promptText;


    public void setupClient()
    {
        canvas.SetActive(false);
        phoneCanvas.SetActive(true);
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
