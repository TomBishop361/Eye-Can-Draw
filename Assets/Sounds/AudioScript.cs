using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{

    public AudioMixer masterMixer;


    public void VolumeChange(Slider slider)
    {                
        masterMixer.SetFloat("MasterVol", (int)slider.value);
    }

   
}
