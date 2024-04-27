using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlidein : MonoBehaviour
{
    public Transform Box;
    public CanvasGroup Background;
    
    public void OnEnableMenu()
    {
        Background.alpha = 0;
        Background.LeanAlpha(1, 0.5f);

        Box.localPosition = new Vector2(0, -Screen.height);
        Box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    // Update is called once per frame
    public void CloseMenu()
    {
        Background.LeanAlpha(0, 0.5f);
        Box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
