using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuORplayagain : MonoBehaviour
{
    //reload scene
    public void playAgain()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    //loads menu scene
    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //loads next level in scenemanager
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
