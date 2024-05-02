
using TMPro;
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

}
