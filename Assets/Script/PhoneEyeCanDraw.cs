using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneEyeCanDraw : MonoBehaviour
{
    [SerializeField] GameObject host;
    [SerializeField] GameObject local;


    private void Start() {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            host.gameObject.SetActive(false);
            local.gameObject.SetActive(false);
        }
    }
    
}
