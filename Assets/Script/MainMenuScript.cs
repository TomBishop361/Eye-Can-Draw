using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayEyeCanDraw()
    {
        SceneManager.LoadScene(1);
    }


    public void quitgame()
    {
        Application.Quit();
    }
}
