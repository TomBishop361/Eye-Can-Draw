using UnityEditor.DeviceSimulation;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField] Canvas main;
    [SerializeField] Canvas connect;
    // Start is called before the first frame update
    void Start()
    {

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            main.gameObject.SetActive(false);
            connect.gameObject.SetActive(true);
            // Run code specific to mobile platforms
            Debug.Log("Running on mobile platform.");
            // Add your mobile-specific code here
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
