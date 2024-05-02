using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phone : MonoBehaviour
{

    void Start()
    {
        //Check is device is handheld Device
        if (SystemInfo.deviceType == DeviceType.Handheld) 
        {
            SceneManager.LoadScene(6);
        }
    }

}

