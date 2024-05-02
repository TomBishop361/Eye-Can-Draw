using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phone : MonoBehaviour
{
    //Detects if phone enters main menu to display correct menu
    void Start()
    {
        //Check is device is handheld Device
        if (SystemInfo.deviceType == DeviceType.Handheld) 
        {
            SceneManager.LoadScene(6);
        }
    }

}

