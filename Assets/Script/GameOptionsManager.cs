using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsManager : MonoBehaviour
{
    bool open = false;
    [SerializeField] RectTransform panel;
    [SerializeField] TMP_Text Timerslider;
    [SerializeField] TMP_Text ScoreSlider;

    //toggle options panel
    public void options()
    {
        if (open)
        {
            panel.gameObject.SetActive(false);
            open = false;
        }else
        {
            panel.gameObject.SetActive(true);
            open = true;
        }

       
    }
  
    //update timer slider
    public void timerChange(Slider slider)
    {
        Timerslider.text = slider.value.ToString();
        Manager.Instance.gameTimer = (int)slider.value;
    }



    //update score slider
    public void ScoreSlier(Slider slider)
    {
        ScoreSlider.text = slider.value.ToString();
        Manager.Instance.scoreLimit = (int)slider.value;
    }

}
