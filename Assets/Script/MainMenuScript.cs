using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayEyeCanDraw()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayEyeCanSolve()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void quitgame()
    {
        Application.Quit();
    }
}
