
using TMPro;
using UnityEngine;

public class ClientScript : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject phoneCanvas;
    [SerializeField] GameObject connectCanvas;
    [SerializeField] TextMeshProUGUI promptText;


    public void setupClient()
    {
        canvas.SetActive(false);
        phoneCanvas.SetActive(true);
        connectCanvas.SetActive(false);
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
